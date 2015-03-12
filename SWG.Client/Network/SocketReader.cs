using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using SWG.Client.Network.Abstracts;
using SWG.Client.Utils;



namespace SWG.Client.Network
{
    public class SocketReader : ServiceBase
    {
        private static readonly LogAbstraction.ILogger _logger = LogAbstraction.LogManagerFacad.GetCurrentClassLogger();

        protected CompressioEncryption _enccom = new CompressioEncryption();
        
        protected int _maxMessageSize = 496;

        protected bool _exit = false;

        public Socket Socket { get; set; }

        public Session Session { get; set; }
        
        public int MaxMessageSize
        {
            get { return _maxMessageSize; }
            set { _maxMessageSize = value; }
        }


        public SocketReader()
        {
            
        }

        public SocketReader(Session Session, Socket socket, bool Start = false)
        {
            this.Session = Session;
            this.Socket = socket;

            ServiceThreadName = "SocketReader Thread";

            if (Start)
            {
                this.Start();
            }
            
        }


        protected override void DoWork()
        {
            Packet recievedPacket;

            int recievedCount = 0;

            SocketError error;
            recievedPacket = new Packet(496);
            recievedCount = Socket.Receive(recievedPacket.Data, 0, MaxMessageSize, SocketFlags.None, out error);

            _logger.Debug("Packet Recieved: {0}",
                              BitConverter.ToString(recievedPacket.Data, 0, recievedCount));

            if (error != SocketError.Success)
            {
                _logger.Error("Socket Error: {0}", error);
            }
            

            if (error == SocketError.ConnectionReset)
            {
                Session.Command = SessionCommand.Disconnect;
                _exit = true;
            }
            else if(error == SocketError.Interrupted) //we closed the socket
            {
                return;
            }

            if (recievedCount > MaxMessageSize || recievedCount == 0)
            {
                _logger.Warn("Recieved count ({0}) larger than max packet size", recievedCount);
                return;
            }

            recievedPacket.Reset();
            recievedPacket.Size = recievedCount;

            byte packetTypeLow = recievedPacket.PeekByte();
            UInt16 packetType = recievedPacket.PeekUInt16();
            SessionOp packetTypeEnum = recievedPacket.PacetTypeEnum;

            if (packetType > 0x00ff && (packetType & 0x00ff) == 0)
            {
                switch (packetTypeEnum)
                {
                    case SessionOp.Disconnect:
                    case SessionOp.DataAck1:
                    case SessionOp.DataAck2:
                    case SessionOp.DataAck3:
                    case SessionOp.DataAck4:
                    case SessionOp.DataOrder1:
                    case SessionOp.DataOrder2:
                    case SessionOp.DataOrder3:
                    case SessionOp.DataOrder4:
                    case SessionOp.Ping:
                        _HandleEncryptedPacket(recievedPacket, recievedCount);
                        break;
                    case SessionOp.MultiPacket:
                    case SessionOp.NetStatRequest:
                    case SessionOp.NetStatResponse:
                    case SessionOp.DataChannel1:
                    case SessionOp.DataChannel2:
                    case SessionOp.DataChannel3:
                    case SessionOp.DataChannel4:
                    case SessionOp.DataFrag1:
                    case SessionOp.DataFrag2:
                    case SessionOp.DataFrag3:
                    case SessionOp.DataFrag4:
                        _HandleCompressedEncryptedPacket(recievedPacket, recievedCount);
                        break;
                    case SessionOp.SessionRequest:
                    case SessionOp.SessionResponse:
                    case SessionOp.FatalError:
                    case SessionOp.FatalErrorResponse:
                        Session.HandleSessionPacket(recievedPacket);
                        break;
                }
            }
            else if (packetTypeLow < 0x0d)
            {
                _HandleFastpathPacket(recievedPacket, recievedCount);
            }

            Thread.Sleep(TimeSpan.FromMilliseconds(0.01));
        }


        protected void _HandleFastpathPacket(Packet recievedPacket, int recievedBytes)
        {
            UInt32 crc = _enccom.GenerateCRC(recievedPacket.Data, Convert.ToUInt32(recievedBytes - 2), Session.EncryptionKey);

            byte crcLow = recievedPacket.Data[recievedBytes - 1];
            byte crcHigh = recievedPacket.Data[recievedBytes - 2];

            if (crcLow != (byte)crc || crcHigh != (byte)(crc >> 8))
            {
                return;
            }

            Packet decompressedPacket = new Packet(496);

            Array.ConstrainedCopy(recievedPacket.Data, 0, decompressedPacket.Data, 0, 2);
            _enccom.Decrypt(recievedPacket.Data, Convert.ToUInt32(recievedBytes - 3), Session.EncryptionKey, 1);

            int decompressedBytes = 0;
            byte compressedFlag = recievedPacket.Data[recievedBytes - 3];
            if (compressedFlag == 1)
            {
                decompressedBytes = _enccom.Decompress(recievedPacket.Data, recievedBytes - 4, decompressedPacket.Data, 492, 1, 1); 
            }

            if (decompressedBytes > 0)
            {
                decompressedPacket.Compressed = true;
                decompressedPacket.Size = decompressedBytes + 1;
                recievedPacket = decompressedPacket;
            }
            else
            {
                recievedPacket.Size -= 3;
            }

            Session.HandleFastpahPacket(recievedPacket);
        }


        protected void _HandleCompressedEncryptedPacket(Packet recievedPacket, int recievedBytes)
        {
            UInt32 crc = _enccom.GenerateCRC(recievedPacket.Data, recievedBytes - 2, Session.EncryptionKey, 0);

            byte packetCrcLow = recievedPacket.Data[recievedBytes - 1];
            byte packetCrcHigh = recievedPacket.Data[recievedBytes - 2];
            byte crcHigh = (byte)(crc >> 8);
            byte crcLow = (byte)crc;

            if (crcLow != packetCrcLow || crcHigh != packetCrcHigh)
            {
                _logger.Warn("Compressed Encrypted Packet dropped due to CRC Mismatch");

                if (recievedPacket.PacetTypeEnum == SessionOp.NetStatResponse)
                {
                    _logger.Debug("Malformed Netstat, ignorning the crc");
                }
                else
                {
                    return;
                }
                
            }

            Packet decompressedPacket = new Packet(496);

            Array.ConstrainedCopy(recievedPacket.Data, 0, decompressedPacket.Data, 0, 2);

            _enccom.Decrypt(recievedPacket.Data, Convert.ToUInt32(recievedBytes - 4), Session.EncryptionKey, 2);
            int decompressedBytes = _enccom.Decompress(recievedPacket.Data, recievedBytes - 5, decompressedPacket.Data, 491, 2, 2);

            if (decompressedBytes > 0)
            {
                decompressedPacket.Compressed = true;
                decompressedPacket.Size = decompressedBytes + 2;
                Session.HandleSessionPacket(decompressedPacket);
            }
            else
            {
                recievedPacket.Size = recievedPacket.Size - 3;
                Session.HandleSessionPacket(recievedPacket);
            }
        }


        protected void _HandleEncryptedPacket(Packet recievedPacket, int recievedBytes)
        {
            UInt32 crc = _enccom.GenerateCRC(recievedPacket.Data, recievedBytes - 2, Session.EncryptionKey);

            byte crcLow = recievedPacket.Data[recievedBytes - 1];
            byte crcHigh = recievedPacket.Data[recievedBytes - 2];

            if (crcLow != (byte)crc || crcHigh != (byte)(crc >> 8))
            {
                _logger.Debug("Dropped encrypted packet due to CRC mismatch");
                return;
            }

            _enccom.Decrypt(recievedPacket.Data, Convert.ToUInt32(recievedBytes - 4), Session.EncryptionKey, 2);

            recievedPacket.Size = recievedBytes - 3;

            Session.HandleSessionPacket(recievedPacket);

        }
    }
}
