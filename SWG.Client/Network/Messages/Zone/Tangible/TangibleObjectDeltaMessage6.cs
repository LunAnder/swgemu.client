using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network.Messages.Zone.Tangible
{
    public class TangibleObjectDeltaMessage6 : DeltaMessage
    {
        public int? ServerId { get; set; }
        public ListChange<long>[] DefenderObjectIds { get; set; } 

        public TangibleObjectDeltaMessage6(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
                return false;

            for (int i = 0; i < UpdateCount; i++)
            {
                var updateId = ReadInt16();
                switch (updateId)
                {
                    case 0x00:
                        ServerId = ReadInt32();
                        break;
                    case 0x01:
                        DefenderObjectIds = ReadLongIndexedListChanges();
                        break;
                }
            }

            return true;
        }
    }
}
