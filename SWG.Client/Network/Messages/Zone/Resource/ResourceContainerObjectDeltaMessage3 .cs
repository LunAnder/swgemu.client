using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone.Resource
{
    [RegisterDeltaMessage(MessageOp.RCNO, 0x03)]
    public class ResourceContainerObjectDeltaMessage3 : DeltaMessage
    {
        public int Quantity { get; set; }

        public ResourceContainerObjectDeltaMessage3()
        {
        }

        public ResourceContainerObjectDeltaMessage3(byte[] Data, int Size, bool parseFromData = false) : base(Data, Size, parseFromData)
        {
        }

        public ResourceContainerObjectDeltaMessage3(Message message, bool parseFromData = false) : base(message, parseFromData)
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
                    case 0x0B:
                        Quantity = ReadInt32();
                        break;
                }
            }

            return true;
        }
    }
}
