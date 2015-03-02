using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network.Messages.Zone
{
    public class ConnectPlayerResponseMessage : Message
    {

        public int Unknown { get; set; }

        public ConnectPlayerResponseMessage(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            ReadIndex = 6;
            Unknown = ReadInt32();
            return true;
        }
    }
}
