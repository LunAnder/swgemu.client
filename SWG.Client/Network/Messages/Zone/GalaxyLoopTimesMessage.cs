using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils;

namespace SWG.Client.Network.Messages.Zone
{
    public class GalaxyLoopTimesMessage : Message
    {
        public int Time1 { get; set; }
        public int Time2 { get; set; }

        public GalaxyLoopTimesMessage() { }

        public GalaxyLoopTimesMessage(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            ReadIndex = 6;
            Time1 = ReadInt32();
            Time2 = ReadInt32();
            return true;
        }
    }
}
