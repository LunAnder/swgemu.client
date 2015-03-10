using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

using SWG.Client.Network;
using SWG.Client.Network.Messages;
using SWG.Client.Network.Messages.Login;
using SWG.Client.Network.Messages.Zone;
using SWG.Client.Network.Objects;
using SWG.Client.Utils;
using Wxv.Swg.Common.Files;
using System.IO;
using SWG.Client.Object.Templates;
using SWG.Client.Object.Templates.Shared;
using SWG.Client.Object.Templates.Shared.Tangible;

namespace Tester
{
    class Program
    {
        private static readonly LogAbstraction.ILogger _logger =
            LogAbstraction.LogManagerFacad.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            /*using(var fileStream = File.OpenRead(@"E:\workspace\tre\patch_05.tre"))
             {
                 var treReader = new TREFileReader();
                 var treeFile = treReader.Load(fileStream);

                 var found = treeFile.InfoFiles.FirstOrDefault(cur => cur.Name.ToLower().Contains("shared_base_object.iff"));

                 if (found != null)
                 {
                     var treeStream = found.Open(fileStream);
                     var iffReader = new IFFFileReader();
                     var iffFile = iffReader.Load(treeStream);
                     //var root = iffFile.Root.Children.ToList();

                     var sharedObject = new SharedObjectTemplate(iffFile);

                 }
             }*/

            //var repo = new ArchiveRepository(@"D:\SWGTrees");
            //repo.LoadArchives();

            //var templateRepo = new TemplateRepository
            //{
            //    ArchiveRepo = repo,
            //};



            //var tmp = repo.LoadCrCMap("misc/object_template_crc_string_table.iff");

            //var last = tmp.CRCMap.Last();

            //var stringFile = repo.LoadStf("string/en/city/city.stf");

            //Console.WriteLine("Enter to end;");
            //Console.ReadLine();
            //return;

            //var addrToConnect = "swgemutest";
            //var connectToServerName = "swgemutest";


            var graph = new ObjectGraph();
            graph.RegisterCreatesFromAssembly();

            var addrToConnect = "login.swgemu.com";
            var connectToServerName = "Basilisk";

            var characterToLogin = "CrazedZealot";
            //var characterToLogin = "Crazyguy";

            Session session = new Session {RequestId = 2341789 };
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            SocketReader reader = new SocketReader(session, socket);
            SocketWriter writer = new SocketWriter(session, socket);

            var addr = Dns.GetHostAddresses(addrToConnect);
            if (addr.Length == 0)
            {
                Console.WriteLine("Failed to resolve '{0}' to an ipaddress", addrToConnect);
                Console.ReadLine();
                return;
            }

            var ip = addr.First();
            Console.WriteLine("resolved {0} to '{1}'", ip, addrToConnect);

            //var ip = IPAddress.Parse("192.168.200.4");

            _logger.Debug("Using ip: {0}", ip);

            var endpoint = new IPEndPoint(ip, 44453);
            
            reader.Socket = socket;
            reader.MaxMessageSize = 496;
            session.Command = SessionCommand.Connect;
            
            socket.Connect(endpoint);
            reader.Start();
            //Console.WriteLine("Enter to start writer");
            //Console.ReadLine();

            writer.Socket = socket;
            writer.Start();

            _logger.Info("Wait for session");
            Console.ReadLine();


            Message loginIDMessage = new Message();

            loginIDMessage.AddData((UInt16)4); //opcode count
            loginIDMessage.AddData(0x41131F96);
            loginIDMessage.AddData("crazyguymrkii", Encoding.ASCII);
            loginIDMessage.AddData("SWGEmu_889656", Encoding.ASCII);
            loginIDMessage.AddData("20050408-18:00", Encoding.ASCII);
            loginIDMessage.Size = loginIDMessage.WriteIndex;



            _logger.Info("sending login id packet");
            Console.ReadLine();

            session.SendChannelA(loginIDMessage);


            _logger.Info("Get recieved messages");
            Console.ReadLine();

            Message msg = null;

            LoginClientTokenMessage clientTokenMsg = null;
            LoginClusterStatusMessage clusterStatus = null;
            LoginEnumClusterMessage enumCluster = null;
            EnumerateCharacterIdMessage enumCharacters = null;
            ErrorMessage error = null;
            while (session.IncomingMessageQueue.Count > 0)
            {
                msg = session.IncomingMessageQueue.Dequeue();
                _logger.Info("Recieved : {0}", msg);

                if (msg.MessageOpCodeEnum == MessageOp.LoginClientToken)
                {
                    clientTokenMsg = new LoginClientTokenMessage(msg,true);
                }
                else if (msg.MessageOpCodeEnum == MessageOp.LoginClusterStatus)
                {
                    clusterStatus = new LoginClusterStatusMessage(msg,true);
                }
                else if (msg.MessageOpCodeEnum == MessageOp.LoginEnumCluster)
                {
                    enumCluster = new LoginEnumClusterMessage(msg,true);
                }
                else if (msg.MessageOpCodeEnum == MessageOp.EnumerateCharacterId)
                {
                    enumCharacters = new EnumerateCharacterIdMessage(msg,true);
                }
                else if(msg.MessageOpCodeEnum == MessageOp.ErrorMessage)
                {
                    error = new ErrorMessage(msg, true);
                    _logger.Error("Error {0}. {1} (Fatal: {2}", error.ErrorType, error.Message, error.Fatal);
                }
                
            }

            if (error == null)
            {
                reader.Suspend();
                writer.Suspend();
                socket.Close();
                _logger.Info("Wait for suspend");
                Console.ReadLine();

                foreach (ServerName serverName in enumCluster.Servers ?? new ServerName[0])
                {
                    var serverData = clusterStatus.Servers.FirstOrDefault(cur => cur.ServerID == serverName.ServerID);
                    _logger.Info("{0} @ {1}:{2}", serverName.ServerDisplayName, serverData.ServerIP ?? "Unknown", serverData.ServerPort);
                }

                ServerDetails server = clusterStatus.Servers.FirstOrDefault(cur => cur.ServerID == enumCluster.Servers.FirstOrDefault(srv => srv.ServerDisplayName == connectToServerName).ServerID);

                if(server == null)
                {
                    _logger.Info("No Servers found");
                    Console.ReadLine();
                    return;
                }

                var toConnect = IPAddress.Parse(server.ServerIP);

                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                reader.Socket = socket;
                writer.Socket = socket;

                session = new Session();

                reader.Session = session;
                writer.Session = session;
                session.Command = SessionCommand.Connect;

                _logger.Debug("Connecting to zone server at {0}:{1}", toConnect,server.ServerPort);
                socket.Connect(toConnect, server.ServerPort);

                reader.Resume();
                writer.Resume();


                _logger.Info("Wait for new session establish");
                Console.ReadLine();

                int idx = 0;
                _logger.Info("hb sessionKey: {0}", clientTokenMsg.SessionKey.ReadUInt32(ref idx));
                idx = 0;
                _logger.Info("nb sessionKey: {0}", clientTokenMsg.SessionKey.ReadNetworkUInt32(ref idx));

                var clientIDMsg = new ClientIDMessage(clientTokenMsg.UserID, clientTokenMsg.SessionKey);
                clientIDMsg.AddFieldsToData();

                _logger.Info("Send client id");
                Console.ReadLine();
                session.SendChannelA(clientIDMsg);

                _logger.Info("Wait for response");
                Console.ReadLine();
                List<Message> messages = new List<Message>();

                if(session.IncomingMessageQueue.Count == 0)
                {
                    _logger.Warn("No messages after client id");
                }

                while (session.IncomingMessageQueue.Count > 0)
                {
                    msg = session.IncomingMessageQueue.Dequeue();

                    _logger.Info("Recieved message after client id req: {0}", msg);

                    switch (msg.MessageOpCodeEnum)
                    {
                            case MessageOp.ErrorMessage:
                                msg = new ErrorMessage(msg,true);
                                _logger.Error(((ErrorMessage)msg).Message);
                            break;
                            case MessageOp.ClientPermissionsMessage:
                                 msg  = new ClientPermissionsMessage(msg, true);
                            break;
                    }
                    
                    messages.Add(msg);
                }


                foreach (var serverChar in enumCharacters.Characters)
                {
                    _logger.Info("available character: {0} ({1})", serverChar.Name, serverChar.CharacterID);
                }

                var character = enumCharacters.Characters.FirstOrDefault(cur => cur.Name == characterToLogin);

                if(character == null)
                {
                    _logger.Error("No character with name {0} to login", characterToLogin);
                    return;
                }

                _logger.Info("Selecting character {0}({1}). Status: {2}", character.Name, character.CharacterID, character.Status);
                
                SelectCharacterMessage selectCharacter = new SelectCharacterMessage
                    {
                        CharacterID = character.CharacterID
                    };

                selectCharacter.AddFieldsToData();


                graph.Session = session;
                graph.Start();
                
                _logger.Info("Wait for graph start");
                Console.ReadLine();
                session.SendChannelA(selectCharacter);
                //session._inSequenceNext = 1;
                _logger.Info("Press enter");
                Console.ReadLine();


                foreach (var item in graph.messages)
                {
                    _logger.Info(item.ToString());
                }


            }

        }
        
    }
}
