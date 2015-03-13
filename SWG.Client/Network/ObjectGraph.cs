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
using SWG.Client.Network.Messages.Login;
using SWG.Client.Network.Messages.Zone;
using SWG.Client.Network.Messages.Zone.Cell;
using SWG.Client.Network.Messages.Zone.Creature;
using SWG.Client.Network.Messages.Zone.Intangible;
using SWG.Client.Network.Messages.Zone.Player;
using SWG.Client.Network.Messages.Zone.Static;
using SWG.Client.Network.Messages.Zone.Tangible;
using SWG.Client.Utils;


namespace SWG.Client.Network
{
    public class ObjectGraph : ServiceBase
    {
        public readonly ConcurrentDictionary<uint, Func<Message, Message>> RegisteredObjects =
            new ConcurrentDictionary<uint, Func<Message, Message>>();

        public readonly ConcurrentDictionary<uint, Func<Message, Message>> RegisteredBaselines =
            new ConcurrentDictionary<uint, Func<Message, Message>>();

        public readonly ConcurrentDictionary<uint, Func<Message, Message>> RegisteredDeltas =
            new ConcurrentDictionary<uint, Func<Message, Message>>();

        //private static readonly LogAbstraction.ILogger _logger = LogAbstraction.LogManagerFacad.GetCurrentClassLogger();
        private static readonly LogAbstraction.ILogger _logger = LogAbstraction.LogManagerFacad.GetCurrentClassLogger();

        //private  Dictionary<long,Message> messages = new Dictionary<long, Message>();
        public List<Message> messages = new List<Message>(); 

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


        public ObjectGraph() 
            : this(true)
        {
            
        }

        public ObjectGraph(bool registerCreateFromAssemblies)
        {
            if (registerCreateFromAssemblies)
            {
                RegisterCreatesFromAssembly();
            }

        }

        protected override void DoWork()
        {
            if (Session == null)
            {
                return;
            }

            while (Session.IncomingMessageQueue.Count > 0)
            {
                var msg = Session.IncomingMessageQueue.Dequeue();

                Func<Message, Message> createFunc = null;
                Message transformed = null;
                if (RegisteredObjects.TryGetValue(msg.MessageOpCode, out createFunc) &&
                    (transformed = createFunc(msg)) != null)
                {
                    messages.Add(createFunc(msg));
                }
                else
                {
                    _logger.Warn("Unable to find registered message factory for {0}", msg.MessageOpCodeEnum);
                }

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
            _SocketReader.Start();
            _SocketWriter.Start();

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
            
            var clientIDMsg = new ClientIDMessage(userId, sessionKey);
            clientIDMsg.AddFieldsToData();

            Session.SendChannelA(clientIDMsg);


            var messages =
                Session.IncomingMessageQueue.WaitForMessages(Convert.ToInt32(ConnectTimeout.TotalMilliseconds),
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

            var permissionMessage = Message.Create<ClientPermissionsMessage>(messages.FirstOrDefault(cur => cur.MessageOpCodeEnum == MessageOp.ClientPermissionsMessage));
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

            Start();          

            var timeout = DateTime.Now.Add(ConnectTimeout);

            while (ServiceStatus != ServiceState.Running)
            {
                if (timeout <= DateTime.Now)
                {
                    throw new TimeoutException("Timeout waiting for server conenction");
                }

                Thread.Sleep(TimeSpan.FromMilliseconds(ConnectTimeout.TotalMilliseconds / 10));
            }

            Session.SendChannelA(selectCharacter);

        }

        #region Parse Baseline

        protected BaselineMessage ParseBaselineMessage(Message Msg)
        {
            Msg.ReadIndex = 14;
            MessageOp primaryType = (MessageOp)Msg.ReadInt32();

            switch (primaryType)
            {
                case MessageOp.CREO:
                    return ParseBaselineCREOMessage(Msg);
                case MessageOp.SCLT:
                    return ParseBaselineSCLTMessage(Msg);
                case MessageOp.PLAY:
                    return ParseBaselinePLAYMessage(Msg);
                case MessageOp.STAO:
                    return ParseBaselineSTAOMessage(Msg);
                case MessageOp.TANO:
                    return ParseBaselineTANOMessage(Msg);
                case MessageOp.ITNO:
                    return ParseBaselineITNOMessage(Msg);
            }

            return null;
        }


        protected BaselineMessage ParseBaselineITNOMessage(Message Msg)
        {
            byte secondayType = Msg.ReadByte();
            switch (secondayType)
            {
                case 0x03:
                    return new IntangibleObjectMessage3(Msg, true);
                case 0x06:
                    return new IntangibleObjectMessage6(Msg, true);
            }
            return null;
        }


        protected BaselineMessage ParseBaselineTANOMessage(Message Msg)
        {
            byte secondayType = Msg.ReadByte();
            switch (secondayType)
            {
                case 0x03:
                    return new TangibleObjectMessage3(Msg, true);
                case 0x06:
                    return new TangibleObjectMessage6(Msg, true);
                case 0x07:
                    return new TangibleObjectMessage7(Msg, true);
            }
            return null;
        }


        protected BaselineMessage ParseBaselineSTAOMessage(Message Msg)
        {
            byte secondayType = Msg.ReadByte();
            switch (secondayType)
            {
                case 0x03:
                    return new StaticObjectMessage3(Msg,true);
            }
            return null;
        }


        protected BaselineMessage ParseBaselinePLAYMessage(Message Msg)
        {
            byte secondayType = Msg.ReadByte();
            switch (secondayType)
            {
                case 0x03:
                    return new PlayerObjectMessage3(Msg,true);
                case 0x06:
                    return new PlayerObjectMessage6(Msg,true);
                case 0x08:
                    return new PlayerObjectMessage8(Msg,true);
                case 0x09:
                    return new PlayerObjectMessage9(Msg,true);
            }
            return null;
        }
        
        protected BaselineMessage ParseBaselineSCLTMessage(Message Msg)
        {
            byte secondayType = Msg.ReadByte();
            switch (secondayType)
            {
                case 0x03:
                    return new CellObjectMessage3(Msg,true);
            }
            return null;
        }


        protected BaselineMessage ParseBaselineCREOMessage(Message Msg)
        {
            byte secondayType = Msg.ReadByte();
            switch (secondayType)
            {
                case 0x01:
                    return new CreatureObjectMessage1(Msg,true);
                case 0x03:
                    return new CreatureObjectMessage3(Msg,true);
                case 0x04:
                    return new CreatureObjectMessage4(Msg,true);
                case 0x06:
                    return new CreatureObjectMessage6(Msg,true);
            }
            return null;
        }
        #endregion

        #region Parse Delta

        protected DeltaMessage ParseDeltaMessage(Message Msg)
        {
            Msg.ReadIndex = 14;
            MessageOp primaryType = (MessageOp)Msg.ReadInt32();

            switch (primaryType)
            {
                case MessageOp.CREO:
                    return ParseDeltaCREOMessage(Msg);
                case MessageOp.SCLT:
                    return ParseDeltaSCLTMessage(Msg);
                case MessageOp.PLAY:
                    return ParseDeltaPLAYMessage(Msg);
                case MessageOp.STAO:
                    return ParseDeltaSTAOMessage(Msg);
                case MessageOp.TANO:
                    return ParseDeltaTANOMessage(Msg);
                case MessageOp.ITNO:
                    return ParseDeltaITNOMessage(Msg);
            }

            return null;
        }


        private DeltaMessage ParseDeltaITNOMessage(Message Msg)
        {
            byte secondayType = Msg.ReadByte();
            switch (secondayType)
            {
                case 0x03:
                    return new IntangibleObjectDeltaMessage3(Msg, true);
            }
            return null;
        }


        private DeltaMessage ParseDeltaTANOMessage(Message Msg)
        {
            byte secondayType = Msg.ReadByte();
            switch (secondayType)
            {
                case 0x03:
                    return new TangibleObjectDeltaMessage3(Msg, true);
                case 0x06:
                    return new TangibleObjectDeltaMessage6(Msg, true);
            }
            return null;
        }


        private DeltaMessage ParseDeltaSTAOMessage(Message Msg)
        {
            byte secondayType = Msg.ReadByte();
            switch (secondayType)
            {
            }
            return null;
        }


        private DeltaMessage ParseDeltaPLAYMessage(Message Msg)
        {
            byte secondayType = Msg.ReadByte();
            switch (secondayType)
            {
                case 0x03:
                    return new PlayerObjectDeltaMessage3(Msg, true);
                case 0x06:
                    return new PlayerObjectDeltaMessage6(Msg, true);
                case 0x08:
                    return new PlayerObjectDeltaMessage8(Msg, true);
                case 0x09:
                    return new PlayerObjectDeltaMessage9(Msg, true);
            }
            return null;
        }


        private DeltaMessage ParseDeltaSCLTMessage(Message Msg)
        {
            byte secondayType = Msg.ReadByte();
            switch (secondayType)
            {
            }
            return null;
        }


        private DeltaMessage ParseDeltaCREOMessage(Message Msg)
        {
            byte secondayType = Msg.ReadByte();
            switch (secondayType)
            {
                case 0x01:
                    return new CreatureObjectDeltaMessage1(Msg, true);
                case 0x03:
                    return new CreatureObjectDeltaMessage3(Msg, true);
                case 0x04:
                    return new CreatureObjectDeltaMessage4(Msg, true);
                case 0x06:
                    return new CreatureObjectDeltaMessage6(Msg, true);
            }
            return null;
        }

        #endregion

        #region Register Messages Region
        public bool RegisterMessageObject(MessageOp opcode, Func<Message, Message> createFunc)
        {
            return RegisterMessageObject((uint)opcode, createFunc);
        }

        public bool RegisterMessageObject(uint opcode, Func<Message, Message> createFunc)
        {
            return RegisteredObjects.TryAdd(opcode, createFunc);
        }

        public bool RegisterMessageObject<T>(uint opcode)
            where T : Message, new()
        {
            return RegisterMessageObject(opcode, (msg) => Message.Create<T>(msg));
        }

        public bool RegisterMessageObject<T>(MessageOp opcode)
            where T : Message, new()
        {
            return RegisterMessageObject<T>((uint) opcode);
        }

        public bool RegisterBaselineObject<T>(uint opcode, uint secondary)
            where T : BaselineMessage, new()
        {
            return RegisteredBaselines.TryAdd(opcode + secondary, (msg) => Message.Create<T>(msg));
        }

        public bool RegisterBaselineObject<T>(MessageOp opcode, uint secondary)
            where T : BaselineMessage, new()
        {
            return RegisterBaselineObject<T>((uint) opcode, secondary);
        }

        public bool RegisterDeltaObject<T>(uint opcode, uint secondary)
            where T : DeltaMessage, new()
        {
            return RegisteredDeltas.TryAdd(opcode + secondary, (msg) => Message.Create<T>(msg));
        }

        public bool RegisterDeltaObject<T>(MessageOp opcode, uint secondary)
            where T : DeltaMessage, new()
        {
            return RegisterDeltaObject<T>((uint)opcode, secondary);
        }


        public void RegisterCreatesFromAssembly()
        {

            RegisterMessageObject(MessageOp.BaselinesMessage, HandleBaselineMessage);
            RegisterMessageObject(MessageOp.DeltasMessage, HandleDeltaMessage);

            var registerMethodInfo = GetType().GetMethod("RegisterMessageObject", new[] { typeof(uint) });

            foreach (
                var type in
                    GetType()
                        .Assembly.GetTypes()
                        .Where(cur => cur.GetCustomAttributes(typeof (RegisterMessageAttribute), false).Length >= 1))
            {
                var attr =
                    (RegisterMessageAttribute)
                        type.GetCustomAttributes(typeof(RegisterMessageAttribute), false).First();

                var genericRegister = registerMethodInfo.MakeGenericMethod(type);
                genericRegister.Invoke(this, new object[] {attr.OpCode});
            }


            registerMethodInfo = GetType().GetMethod("RegisterBaselineObject", new[] { typeof(uint), typeof(uint) });

            foreach (
                var type in
                    GetType()
                        .Assembly.GetTypes()
                        .Where(cur => cur.GetCustomAttributes(typeof(RegisterBaselineMessageAttribute), false).Length >= 1))
            {
                var attr =
                    (RegisterBaselineMessageAttribute)
                        type.GetCustomAttributes(typeof(RegisterBaselineMessageAttribute), false).First();

                var genericRegister = registerMethodInfo.MakeGenericMethod(type);
                genericRegister.Invoke(this, new object[] { attr.OpCode, attr.Secondary.GetValueOrDefault()  });
            }

            registerMethodInfo = GetType().GetMethod("RegisterDeltaObject", new[] { typeof(uint), typeof(uint) });

            foreach (
                var type in
                    GetType()
                        .Assembly.GetTypes()
                        .Where(cur => cur.GetCustomAttributes(typeof(RegisterDeltaMessageAttribute), false).Length >= 1))
            {
                var attr =
                    (RegisterDeltaMessageAttribute)
                        type.GetCustomAttributes(typeof(RegisterDeltaMessageAttribute), false).First();


                var genericRegister = registerMethodInfo.MakeGenericMethod(type);
                genericRegister.Invoke(this, new object[] { attr.OpCode, attr.Secondary.GetValueOrDefault() });
            }


        }

        protected virtual Message HandleBaselineMessage(Message message)
        {
            message.ReadIndex = 14;
            uint opcode = message.ReadUInt32();
            uint secondary = message.ReadByte();
            uint key = opcode + secondary;

            Func<Message, Message> createFactory = null;
            if (RegisteredBaselines.TryGetValue(key, out createFactory))
            {
                return createFactory(message);
            }

            return null;
        }

        protected virtual Message HandleDeltaMessage(Message message)
        {
            message.ReadIndex = 14;
            uint opcode = message.ReadUInt32();
            uint secondary = message.ReadByte();
            uint key = opcode + secondary;

            Func<Message, Message> createFactory = null;
            if (RegisteredDeltas.TryGetValue(key, out createFactory))
            {
                return createFactory(message);
            }

            return null;
        }

        #endregion

    }
}
