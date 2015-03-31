using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Chat
{
    [RegisterMessage(MessageOp.ChatOnEnteredRoom)]
    public class ChatOnEnteredRoomMessage : Message
    {
        public string Application { get; set; }
        public string Server { get; set; }
        public string PlayerName { get; set; }
        public long ChannelId { get; set; }
        
        #region ctor

        public ChatOnEnteredRoomMessage()
        {
        }

        public ChatOnEnteredRoomMessage(uint Size) : base(Size)
        {
        }

        public ChatOnEnteredRoomMessage(int Size) : base(Size)
        {
        }

        public ChatOnEnteredRoomMessage(byte[] Data, int Size = 0, bool ParseFromData = false)
            : base(Data, Size, ParseFromData)
        {
        }

        public ChatOnEnteredRoomMessage(byte[] Data, int StartIndex, int Length, int Size = 0,
            bool ParseFromData = false) : base(Data, StartIndex, Length, Size, ParseFromData)
        {
        }

        public ChatOnEnteredRoomMessage(Message ToCreateFrom, bool ParseFromData = false)
            : base(ToCreateFrom, ParseFromData)
        {
        }

        public ChatOnEnteredRoomMessage(Message ToCreateFrom, int StartIndex, int Length, bool ParseFromData = false)
            : base(ToCreateFrom, StartIndex, Length, ParseFromData)
        {
        }

        public ChatOnEnteredRoomMessage(Message ToCreateFrom, int StartIndex, int Length, int Size = 0,
            bool ParseFromData = false) : base(ToCreateFrom, StartIndex, Length, Size, ParseFromData)
        {
        }

        #endregion

        public override bool ParseFromData()
        {
            ReadIndex = 6;

            Application = ReadString(Encoding.ASCII);
            Server = ReadString(Encoding.ASCII);
            PlayerName = ReadString(Encoding.ASCII);
            SetReadIntForwardBy(1);
            ChannelId = ReadInt64();

            return true;
        }
    }
}
