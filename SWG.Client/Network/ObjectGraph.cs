using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using SWG.Client.Network.Abstracts;
using SWG.Client.Network.Messages;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Network.Messages.Login;
using SWG.Client.Network.Messages.Zone;
using SWG.Client.Network.Messages.Zone.Cell;
using SWG.Client.Network.Messages.Zone.Creature;
using SWG.Client.Network.Messages.Zone.Intangible;
using SWG.Client.Network.Messages.Zone.Player;
using SWG.Client.Network.Messages.Zone.Static;
using SWG.Client.Network.Messages.Zone.Tangible;
using SWG.Client.Object.Templates;
using SWG.Client.Utils;
using SWG.Client.Utils.Attribute;


namespace SWG.Client.Network
{
    public class ObjectGraph : ServiceBase, IHasFallbackMessageFactory, IHasMessageFactories
    {


        public readonly ConcurrentDictionary<uint, Func<Message, Message>> RegisteredBaselines =
            new ConcurrentDictionary<uint, Func<Message, Message>>();

        public readonly ConcurrentDictionary<uint, Func<Message, Message>> RegisteredDeltas =
            new ConcurrentDictionary<uint, Func<Message, Message>>();

        public ConcurrentDictionary<uint, IMessageParseFactory> MessageFactories { get; set; }

        public IMessageParseFactory FallbackFactory { get; set; }


        //private static readonly LogAbstraction.ILogger _logger = LogAbstraction.LogManagerFacad.GetCurrentClassLogger();
        private static readonly LogAbstraction.ILogger _logger = LogAbstraction.LogManagerFacad.GetCurrentClassLogger();
        private static readonly LogAbstraction.ILogger _creatObjlogger = LogAbstraction.LogManagerFacad.GetLogger("ObjectCreateLogger");
        private static readonly LogAbstraction.ILogger _msgLogger = LogAbstraction.LogManagerFacad.GetLogger("MsgLogger");
        //private  Dictionary<long,Message> messages = new Dictionary<long, Message>();
        public List<Message> Messages = new List<Message>(); 

        public Session Session { get; set; }

        public TimeSpan ConnectTimeout { get; set; }

        public bool? SelectedGalaxyOpen { get; private set; }

        public bool? CanCreateCharacterOnGalaxy { get; private set; }

        public ulong? LoggedInCharacterId { get; private set; }


        public IPAddress ConnectedAddress { get; private set; }
        public int? ConnectedPort { get; private set; }

        protected SocketReader _SocketReader;
        protected SocketWriter _SocketWriter;
        protected Socket _Socket;

        protected ITemplateRepository _TemplateRepository = null;

        protected Dictionary<uint, string> _crcMap = null; 


        public ObjectGraph()
        {
            MessageFactories = new ConcurrentDictionary<uint, IMessageParseFactory>();

            
            var archiveRepo = new ArchiveRepository(@"D:\SWGTrees");
            archiveRepo.LoadArchives();
            _TemplateRepository = new TemplateRepository
            {
                ArchiveRepo = archiveRepo,
            };

            

            _crcMap = _TemplateRepository.LoadCrCMap("misc/object_template_crc_string_table.iff").CRCMap;

        }

        protected override void DoWork()
        {
            if (Session == null)
            {
                return;
            }

            while (Session.IncomingMessageQueue.Count > 0)
            {
                Message msg;

                if (!Session.IncomingMessageQueue.TryDequeue(out msg))
                {
                    continue;
                }

                Message transformed;
                IMessageParseFactory factory;
                if (!MessageFactories.TryGetValue(msg.MessageOpCode, out factory) || !factory.TryParse(msg.MessageOpCode, msg, out transformed) )
                {
                    if (!FallbackFactory.TryParse(msg.MessageOpCode, msg, out transformed))
                    {
                        long tmpVal;
                        if (long.TryParse(msg.MessageOpCodeEnum.ToString(), out tmpVal))
                        {
                             _msgLogger.Trace(BitConverter.ToString(msg.Data).Replace("-", ""));
                             _msgLogger.Trace("{0}", msg.OpcodeCount);
                             _msgLogger.Trace("{0}", msg.SourcePacketType);
                        }
                        continue;
                    }    
                }
                
                Messages.Add(transformed);


                //Func<Message, Message> createFunc = null;
                //Message transformed = null;
                //if (RegisteredObjects.TryGetValue(msg.MessageOpCode, out createFunc) &&
                //    (transformed = createFunc(msg)) != null)
                //{
                //    Messages.Add(transformed);

                SceneCreateObject obj = transformed as SceneCreateObject;
                if (obj != null)
                {
                    string objToCreate = null;
                    if (_crcMap.TryGetValue((uint)obj.ObjectCRC, out objToCreate))
                    {
                        _creatObjlogger.Debug("create obj {0}", objToCreate);
                    }
                }

                if (msg.MessageOpCodeEnum == MessageOp.CmdStartScene)
                {
                    _logger.Info("Got scene start! Trn: {0}", ((SceneStart)transformed).TerrainMap);
                }
                //}
                //else
                //{
                //    _logger.Warn("Unable to find registered message factory for {0}({1:X})", msg.MessageOpCodeEnum, msg.MessageOpCode);
                //}

            }

            Thread.Sleep(300);
        }


        public void EstablishConnection(uint userId, byte[] sessionKey, IPAddress address, int port)
        {

            ConnectedAddress = address;
            ConnectedPort = port;
            Session = new Session();
            _Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _SocketReader = new SocketReader(Session, _Socket);
            _SocketWriter = new SocketWriter(Session,  _Socket);
            Session.Command = SessionCommand.Connect;

            _logger.Debug("Connecting to zone server at {0}:{1}", address, port);

            _Socket.Connect(address, port);
            _SocketReader.Start().WaitOne(ConnectTimeout);
            _SocketWriter.Start().WaitOne(ConnectTimeout);

            var timeout = DateTime.Now.Add(ConnectTimeout);

            while (Session.Status != SessionStatus.Connected && Session.Status != SessionStatus.Error)
            {
                if (timeout <= DateTime.Now)
                {
                    _logger.Error("Timeout waiting for connecting to establish");
                    throw new TimeoutException("Timeout waiting for server conenction");
                }

                Thread.Sleep(TimeSpan.FromMilliseconds(ConnectTimeout.TotalMilliseconds / 10));
            }

            if (!Start().WaitOne(ConnectTimeout))
            {
                _logger.Error("ObjectGraph service failed to start in time");
                throw new TimeoutException("Timeout waiting for server conenction");
            }


            var clientIDMsg = new ClientIDMessage(userId, sessionKey);
            clientIDMsg.AddFieldsToData();

            Session.SendChannelA(clientIDMsg);


            var messages =
                Messages.WaitForMessages(Convert.ToInt32(ConnectTimeout.TotalMilliseconds),
                    MessageOp.ErrorMessage, MessageOp.ClientPermissionsMessage);

            if (messages == null)
            {
                _logger.Error("Timeout waiting for ClientPermissionsMessage");
                throw new TimeoutException("Timeout waiting for server connection");
            }

            var errorMsg =
                Message.Create<ErrorMessage>(
                    messages.FirstOrDefault(cur => cur.MessageOpCodeEnum == MessageOp.ErrorMessage));
            if (errorMsg != null)
            {
                _logger.Error("Error getting client permission: [{0}] {1}", errorMsg.ErrorType, errorMsg.Message);
                throw new Exception(string.Format("[{0}] {1}", errorMsg.ErrorType, errorMsg.Message));
            }

            var permissionMessage =
                Message.Create<ClientPermissionsMessage>(
                    messages.FirstOrDefault(cur => cur.MessageOpCodeEnum == MessageOp.ClientPermissionsMessage));
            if (permissionMessage == null)
            {
                _logger.Error("Unknow packet when expecting ClientPermissionsMessage");
                throw new Exception("Unknown error connecting to server");
            }

            SelectedGalaxyOpen = permissionMessage.GalaxyOpenFlag == 1;
            CanCreateCharacterOnGalaxy = permissionMessage.CharacterSlotOpenFlag == 1;
        }

        public void LoginCharacter(long chaaracterId)
        {
            SelectCharacterMessage selectCharacter = new SelectCharacterMessage
            {
                CharacterID = chaaracterId,
            };

            selectCharacter.AddFieldsToData();
            
            Session.SendChannelA(selectCharacter);

        }
    }
}
