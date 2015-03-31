using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone.Resource
{
    [RegisterDeltaMessage(MessageOp.RCNO, 0x06)]
    public class ResourceContainerObjectDeltaMessage6 : DeltaMessage
    {
        public string ResourceName { get; set; }
        public string ResourceType { get; set; }

        public ResourceContainerObjectDeltaMessage6(byte[] Data, int Size, bool parseFromData = false) : base(Data, Size, parseFromData)
        {
        }

        public ResourceContainerObjectDeltaMessage6()
        {
        }

        public ResourceContainerObjectDeltaMessage6(Message message, bool parseFromData = false) : base(message, parseFromData)
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
                    case 0x06:
                        ResourceName = ReadString(Encoding.UTF8);
                        break;
                    case 0x05:
                        ResourceType = ReadString(Encoding.UTF8);
                        break;
                }
            }

            return true;
        }
    }
}
