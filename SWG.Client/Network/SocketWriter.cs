using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using SWG.Client.Network.Abstracts;
using SWG.Client.Utils;



namespace SWG.Client.Network
{
    public class SocketWriter : ServiceBase
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        protected CompressioEncryption _enccom = new CompressioEncryption();

        protected Thread Thread = null;
        
        protected byte[] SendBuffer = new byte[496];

        public UInt64 LastTime { get; protected set; }

        public Socket Socket { get; set; }
        public Session Session { get; set; }


        public SocketWriter() {}
        
        public SocketWriter(Session Session, Socket Socket, bool Start = false)
        {
            this.Session = Session;
            this.Socket = Socket;

            ServiceThreadName = "SocketWriter Thread";
            
            if (Start)
            {
                this.Start();
            }
        }


        protected override void DoWork()
        {
            Packet toSend = null;

            Session.ProcessWriteThread();
            
            while ((toSend = Session.GetOutgoingReliablePacket()) != null)
            {
                _SendPacket(toSend);
            }

            while ((toSend = Session.GetOutgoingUnreliablePacket()) != null)
            {
                _SendPacket(toSend);
            }

            Thread.Sleep(1000);
        }

        public void _SendPacket(Packet ToSend)
        {
            Int32 outlen = 0;

            ToSend.ReadIndex = 0;

            var packetType = ToSend.PacetTypeEnum;
            
            
            byte packetTypeLow = ToSend.PeekByte();

            Array.ConstrainedCopy(ToSend.Data, 0, SendBuffer, 0, 2);

            if (packetType == SessionOp.SessionRequest || packetType == SessionOp.CriticalError)
            {
                Socket.Send(ToSend.Data, ToSend.Size, SocketFlags.None);
                return;
            }


            if (ToSend.Compressed)
            {
                var offset = packetTypeLow == 0 ? 2 : 1;
                outlen = _enccom.Compress(ToSend.Data,
                                          ToSend.Size - offset,
                                          SendBuffer,
                                          SendBuffer.Length - offset,
                                          offset,
                                          offset);

                if (outlen != 0)
                {
                    SendBuffer[outlen + offset] = 0x1;
                    outlen += offset + 1;
                }
                else
                {
                    Array.ConstrainedCopy(ToSend.Data, 0, SendBuffer, 0, ToSend.Size);
                    outlen = ToSend.Size;
                    SendBuffer[outlen] = 0x0;
                    outlen++;
                }
            }
            else
            {
                Array.ConstrainedCopy(ToSend.Data, 0, SendBuffer, 0, ToSend.Size);
                outlen = ToSend.Size;
                SendBuffer[outlen] = 0x0;
                outlen += 1;
            }
            
            if (ToSend.Encrypted)
            {
                var offset = packetTypeLow == 0 ? 2 : 1;

                if (packetTypeLow == 0 || packetTypeLow < 0x0d)
                {
                    _enccom.Encrypt(SendBuffer,
                                    outlen - offset,
                                    Session.EncryptionKey,
                                    offset);
                }


                ToSend.CRC = _enccom.GenerateCRC(SendBuffer, outlen, Session.EncryptionKey, 0);

                SendBuffer[outlen] = (byte)(ToSend.CRC >> 8);
                SendBuffer[outlen + 1] = (byte)ToSend.CRC;

                outlen += 2;
            }
            else if (ToSend.SendCRC)
            {
                ToSend.CRC = _enccom.GenerateCRC(SendBuffer, outlen, Session.EncryptionKey, 0);
                SendBuffer[outlen] = (byte)(ToSend.CRC >> 8);
                SendBuffer[outlen + 1] = (byte)ToSend.CRC;
                outlen += 2;
            }
            
            
            SocketError error;
            Socket.Send(SendBuffer,0, outlen, SocketFlags.None, out error);
            
            if (error != SocketError.Success)
            {
                _logger.Error("Error sending data: {0}", error);
            }
        }
    }
}
