using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone.ManufactureSchematic
{
    [RegisterDeltaMessage(MessageOp.MSCO, 0x06)]
    public class ManufactureSchematicObjectDeltaMessage6 : DeltaMessage
    {

        public int ServerId { get; set; }
        public string CustomizationString { get; set; }
        public string CustomizationModel { get; set; }
        public int PrototypeCrc { get; set; }
        public byte Active { get; set; }
        public byte SlotCount { get; set; }

        public ManufactureSchematicObjectDeltaMessage6()
        {
        }

        public ManufactureSchematicObjectDeltaMessage6(byte[] Data, int Size, bool parseFromData = false) : base(Data, Size, parseFromData)
        {
        }

        public ManufactureSchematicObjectDeltaMessage6(Message message, bool parseFromData = false) : base(message, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
            {
                return false;
            }

            for (int i = 0; i < UpdateCount; i++)
            {
                var updateId = ReadInt16();
                switch (updateId)
                {
                    case 0x00:
                        ServerId = ReadInt32();
                        break;
                    case 0x01:
                        CustomizationString = ReadString(Encoding.ASCII);
                        break;
                    case 0x02:
                        CustomizationModel = ReadString(Encoding.ASCII);
                        break;
                    case 0x03:
                        PrototypeCrc = ReadInt32();
                        break;
                    case 0x04:
                        Active = ReadByte();
                        break;
                    case 0x05:
                        SlotCount = ReadByte();
                        break;
                }

            }

            return true;
        }
    }
}
