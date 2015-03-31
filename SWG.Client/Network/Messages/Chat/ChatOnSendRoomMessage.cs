using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Chat
{
    [RegisterMessage(MessageOp.ChatOnSendRoomMessage)]
    public class ChatOnSendRoomMessage : Message
    {
        public int MessageCode { get; set; }
        public int MessageSequence { get; set; }

        #region ctor

        public ChatOnSendRoomMessage()
        {
        }

        public ChatOnSendRoomMessage(uint Size) : base(Size)
        {
        }

        public ChatOnSendRoomMessage(int Size) : base(Size)
        {
        }

        public ChatOnSendRoomMessage(byte[] Data, int Size = 0, bool ParseFromData = false)
            : base(Data, Size, ParseFromData)
        {
        }

        public ChatOnSendRoomMessage(byte[] Data, int StartIndex, int Length, int Size = 0, bool ParseFromData = false)
            : base(Data, StartIndex, Length, Size, ParseFromData)
        {
        }

        public ChatOnSendRoomMessage(Message ToCreateFrom, bool ParseFromData = false)
            : base(ToCreateFrom, ParseFromData)
        {
        }

        public ChatOnSendRoomMessage(Message ToCreateFrom, int StartIndex, int Length, bool ParseFromData = false)
            : base(ToCreateFrom, StartIndex, Length, ParseFromData)
        {
        }

        public ChatOnSendRoomMessage(Message ToCreateFrom, int StartIndex, int Length, int Size = 0,
            bool ParseFromData = false) : base(ToCreateFrom, StartIndex, Length, Size, ParseFromData)
        {
        }

        #endregion

        public override bool ParseFromData()
        {
            ReadIndex = 6;
            MessageCode = ReadInt32();
            MessageSequence = ReadInt32();

            return true;
        }
    }
}
