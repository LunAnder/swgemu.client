using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Chat
{
    [RegisterMessage(MessageOp.ChatPersistentMessageToClient)]
    public class ChatPersistentMessageToClient : Message
    {

        public string SenderName { get; set; }
        public string Applicaiton { get; set; }
        public string GalaxyName { get; set; }
        public int MailId { get; set; }
        public byte BodySent { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public byte Status { get; set; }
        public int Timestamp { get; set; }


        #region ctor

        public ChatPersistentMessageToClient()
        {
        }

        public ChatPersistentMessageToClient(uint Size) : base(Size)
        {
        }

        public ChatPersistentMessageToClient(int Size) : base(Size)
        {
        }

        public ChatPersistentMessageToClient(byte[] Data, int Size = 0, bool ParseFromData = false)
            : base(Data, Size, ParseFromData)
        {
        }

        public ChatPersistentMessageToClient(Message ToCreateFrom, bool ParseFromData = false)
            : base(ToCreateFrom, ParseFromData)
        {
        }

        public ChatPersistentMessageToClient(byte[] Data, int StartIndex, int Length, int Size = 0,
            bool ParseFromData = false) : base(Data, StartIndex, Length, Size, ParseFromData)
        {
        }

        public ChatPersistentMessageToClient(Message ToCreateFrom, int StartIndex, int Length,
            bool ParseFromData = false) : base(ToCreateFrom, StartIndex, Length, ParseFromData)
        {
        }

        public ChatPersistentMessageToClient(Message ToCreateFrom, int StartIndex, int Length, int Size = 0,
            bool ParseFromData = false) : base(ToCreateFrom, StartIndex, Length, Size, ParseFromData)
        {
        }

        #endregion

        public override bool ParseFromData()
        {
            ReadIndex = 6;

            SenderName = ReadString(Encoding.ASCII);
            Applicaiton = ReadString(Encoding.ASCII);
            GalaxyName = ReadString(Encoding.ASCII);
            MailId = ReadInt32();
            BodySent = ReadByte();
            Body = ReadString(Encoding.ASCII);
            Subject = ReadString(Encoding.ASCII);
            Status = ReadByte();
            Timestamp = ReadInt32();

            return true;

        }
    }
}
