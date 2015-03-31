using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Chat
{
    [RegisterMessage(MessageOp.ChatRoomMessage)]
    public class ChatRoomMessage : Message
    {
        public string Application { get; set; }
        public string Server { get; set; }
        public string CharacterName { get; set; }
        public int RoomId { get; set; }
        public string Message { get; set; }
        public string OOBData { get; set; }

        #region ctor

        public ChatRoomMessage()
        {
        }

        public ChatRoomMessage(uint Size) : base(Size)
        {
        }

        public ChatRoomMessage(int Size) : base(Size)
        {
        }

        public ChatRoomMessage(byte[] Data, int Size = 0, bool ParseFromData = false) : base(Data, Size, ParseFromData)
        {
        }

        public ChatRoomMessage(byte[] Data, int StartIndex, int Length, int Size = 0, bool ParseFromData = false)
            : base(Data, StartIndex, Length, Size, ParseFromData)
        {
        }

        public ChatRoomMessage(Message ToCreateFrom, bool ParseFromData = false) : base(ToCreateFrom, ParseFromData)
        {
        }

        public ChatRoomMessage(Message ToCreateFrom, int StartIndex, int Length, bool ParseFromData = false)
            : base(ToCreateFrom, StartIndex, Length, ParseFromData)
        {
        }

        public ChatRoomMessage(Message ToCreateFrom, int StartIndex, int Length, int Size = 0,
            bool ParseFromData = false) : base(ToCreateFrom, StartIndex, Length, Size, ParseFromData)
        {
        }

        #endregion

        public override bool ParseFromData()
        {
            ReadIndex = 6;

            Application = ReadString(Encoding.ASCII);
            Server = ReadString(Encoding.ASCII);
            CharacterName = ReadString(Encoding.ASCII);
            RoomId = ReadInt32();
            Message = ReadString(Encoding.UTF8);
            OOBData = ReadString(Encoding.UTF8);
            return true;
        }
    }
}
