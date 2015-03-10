using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Threading;
using SWG.Client.Network.Messages;
using SWG.Client.Network.Messages.Login;
using SWG.Client.Network.Objects;
using SWG.Client.Utils;
using UnityEngine;

namespace SWG.Client.Network
{
    public class SessionLoginManager
    {
        public event EventHandler<SessionLoginCompleteEventArgs> LoginComplete;  

        private SocketReader _socketReader = null;
        private SocketWriter _socketWriter = null;
        private Socket _socket = null;


        private static readonly LogAbstraction.ILogger _logger =
            LogAbstraction.LogManagerFacad.GetCurrentClassLogger();

        public const string ClientVersion = "20050408-18:00";

        public Session Session { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Server { get; set; }

        public int Timeout { get; set; }


        public byte[] SessionKey { get; private set; }

        public AvailableServer[] AvailableServers { get; private set; }


        public SessionLoginManager()
        {
            LoginComplete += delegate { };
        }

        public void ConnectToLoginServer(string server = null)
        {
            if (!string.IsNullOrEmpty(server))
            {
                Server = server;
            }

            if (string.IsNullOrEmpty(server))
            {
                throw new ArgumentException("Server");
            }

            if (Session == null)
            {
                Session = new Session();
            }

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socketReader = new SocketReader(Session, _socket);
            _socketWriter = new SocketWriter(Session, _socket);

            var addr = Dns.GetHostAddresses(Server);

            if (addr.Length == 0)
            {   
                _logger.Error("Failed to resolve '{0}' to an ipaddress", Server);
                return;
            }

            var ip = addr.First();
            _logger.Trace("resolved {0} to '{1}'", ip, Server);

            var endpoint = new IPEndPoint(ip, 44453);
            
            _socketReader.MaxMessageSize = 496;
            Session.Command = SessionCommand.Connect;

            _socket.Connect(endpoint);
            _socketReader.Start();
            _socketWriter.Start();

            var timeout = DateTime.Now.AddMilliseconds(Timeout);

            while (Session.Status != SessionStatus.Connected && Session.Status != SessionStatus.Error)
            {
                if (timeout <= DateTime.Now)
                {
                    throw new TimeoutException("Timeout waiting for server conenction");
                }
                Thread.Sleep(Timeout/10);
            }
        }

        public void LoginToServer(string username = null, string password = null)
        {
            if (!string.IsNullOrEmpty(username))
            {
                Username = username;
            }

            if (string.IsNullOrEmpty(Username))
            {
                throw new ArgumentException("Username");
            }

            if (!string.IsNullOrEmpty(password))
            {
                Password = password;
            }

            if (string.IsNullOrEmpty(Password))
            {
                throw new ArgumentException("Password");
            }

            Message loginIDMessage = new Message();

            loginIDMessage.AddData((UInt16)4); //opcode count
            loginIDMessage.AddData(0x41131F96);
            loginIDMessage.AddData(Username, Encoding.ASCII);
            loginIDMessage.AddData(Password, Encoding.ASCII);
            loginIDMessage.AddData(ClientVersion, Encoding.ASCII);
            loginIDMessage.Size = loginIDMessage.WriteIndex;

            _logger.Trace("sending login id packet");

            Session.SendChannelA(loginIDMessage);



            var messages = Session.IncomingMessageQueue.WaitForMessages(Timeout, typeof(ErrorMessage),
                typeof (LoginClientTokenMessage), typeof (LoginClusterStatusMessage), typeof (LoginEnumClusterMessage),
                typeof (LoginEnumClusterMessage));

            if (messages == null)
            {
                throw  new TimeoutException("Timeout waiting for login");
            }


            ErrorMessage error = null;
            LoginClientTokenMessage clientTokenMsg = null;
            LoginClusterStatusMessage clusterStatus = null;
            LoginEnumClusterMessage enumCluster = null;
            EnumerateCharacterIdMessage enumCharacters = null;

            foreach (var message in messages)
            {
                var errorMessage = message as ErrorMessage;
                if (errorMessage != null)
                {
                    error = errorMessage;
                    break;
                }

                var msg = message as LoginClientTokenMessage;
                if (msg != null)
                {
                    clientTokenMsg = msg;
                }

                var status = message as LoginClusterStatusMessage;
                if (status != null)
                {
                    clusterStatus = status;
                }


                var cluster = message as LoginEnumClusterMessage;
                if (cluster != null)
                {
                    enumCluster = cluster;
                }

                var characters = message as EnumerateCharacterIdMessage;
                if (characters != null)
                {
                    enumCharacters = characters;
                }
            }

            if (error != null)
            {
                throw new Exception(string.Format("[{0}] {1}", error.ErrorType, error.Message));
            }

            if (clientTokenMsg == null || clusterStatus == null || enumCluster == null || enumCharacters == null)
            {
                throw new Exception("Missing require message after login");
            }

            SessionKey = clientTokenMsg.SessionKey;

            AvailableServers = 
                Array.ConvertAll(clusterStatus.Servers,
                    cur =>
                        new AvailableServer(cur,
                            enumCluster.Servers.First(serverName => serverName.ServerID == cur.ServerID),
                            enumCharacters.Characters.Where(character => character.ServerID == cur.ServerID)));

            Stop();

            LoginComplete(this, new SessionLoginCompleteEventArgs(SessionKey, AvailableServers));
        }


        public void Stop()
        {
            _socketReader.Stop();
            _socketReader = null;
            _socketWriter.Stop();
            _socketWriter = null;
            _socket.Close();
            _socket = null; 
        }

       

    }
}
