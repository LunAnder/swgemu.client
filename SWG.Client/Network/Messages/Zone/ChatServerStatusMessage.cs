using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone
{
    [RegisterMessage(MessageOp.ChatServerStatus)]
    public class ChatServerStatusMessage : Message
    {

        public byte Online { get; set; }

        public ChatServerStatusMessage() { }

        public ChatServerStatusMessage(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            ReadIndex = 6;
            Online = ReadByte();
            return true;
        }
    }
}
