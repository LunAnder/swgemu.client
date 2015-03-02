using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network.Messages.Zone
{
    public class ParametersMessage : Message
    {

        public int Flags { get; set; }

        public ParametersMessage(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            ReadIndex = 6;
            Flags = ReadInt32();
            return true;
        }
    }
}
