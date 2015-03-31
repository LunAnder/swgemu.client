using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils;

namespace SWG.Client.Network.Messages.Zone.Resource
{
    [RegisterBaselineMessage(MessageOp.RCNO, 0x03)]
    public class ResourceContainerObjectMessage3 : Tangible.TangibleObjectMessage3
    {
        public int Quantity { get; set; }
        public long SpawnId { get; set; }

        public ResourceContainerObjectMessage3()
        {
        }

        public ResourceContainerObjectMessage3(byte[] Data, int Size, bool parseFromData = false) : base(Data, Size, parseFromData)
        {
        }

        public ResourceContainerObjectMessage3(Message message, bool parseFromData = false) : base(message, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
            {
                return false;
            }

            Quantity = ReadInt32();
            SpawnId = ReadInt64();

            return true;
        }
    }
}
