using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network.Messages.Zone.Tangible
{
    public class UpdatePVPStatusMessage : Message
    {
        public int PVPStatus { get; set; }
        public int Faction { get; set; }
        public long ObjectId { get; set; }

        public UpdatePVPStatusMessage(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            ReadIndex = 6;

            PVPStatus = ReadInt32();
            Faction = ReadInt32();
            ObjectId = ReadInt64();

            return true;
        }
    }
}
