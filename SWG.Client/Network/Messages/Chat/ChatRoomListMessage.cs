using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using SWG.Client.Object.Chat;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Chat
{
    [RegisterMessage(MessageOp.ChatRoomlist)]
    public class ChatRoomListMessage : Message
    {
        public ChatRoom[] ChatRooms { get; set; }

        #region ctor

        public ChatRoomListMessage()
        {
        }

        public ChatRoomListMessage(uint Size) : base(Size)
        {
        }

        public ChatRoomListMessage(byte[] Data, int Size = 0, bool ParseFromData = false)
            : base(Data, Size, ParseFromData)
        {
        }

        public ChatRoomListMessage(int Size) : base(Size)
        {
        }

        public ChatRoomListMessage(byte[] Data, int StartIndex, int Length, int Size = 0, bool ParseFromData = false)
            : base(Data, StartIndex, Length, Size, ParseFromData)
        {
        }

        public ChatRoomListMessage(Message ToCreateFrom, bool ParseFromData = false) : base(ToCreateFrom, ParseFromData)
        {
        }

        public ChatRoomListMessage(Message ToCreateFrom, int StartIndex, int Length, bool ParseFromData = false)
            : base(ToCreateFrom, StartIndex, Length, ParseFromData)
        {
        }

        public ChatRoomListMessage(Message ToCreateFrom, int StartIndex, int Length, int Size = 0,
            bool ParseFromData = false) : base(ToCreateFrom, StartIndex, Length, Size, ParseFromData)
        {
        }

        #endregion

        public override bool ParseFromData()
        {
            ReadIndex = 6;

            var chatRoomCount = ReadInt32();
            ChatRooms = new ChatRoom[chatRoomCount];

            for (int i = 0; i < chatRoomCount; i++)
            {
                ChatRooms[i] = new ChatRoom().Deserialize(this);
            }

            return true;
        }
    }
}
