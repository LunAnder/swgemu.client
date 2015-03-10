using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network.Messages
{
    public class LinkDeadMessage : Message
    {
        

        public LinkDeadMessage(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            ReadIndex = 6;
            return true;
        }
    }
}
