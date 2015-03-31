using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Object.ManufactureSchematic.CraftingValues;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone.ManufactureSchematic
{
    [RegisterDeltaMessage(MessageOp.MSCO, 0x03)]
    public class ManufactureSchematicObjectDeltaMessage3 : DeltaMessage
    {
        public float Complexity { get; set; }
        public string STFFile { get; set; }
        public string STFName { get; set; }
        public string CustomName { get; set; }
        public int Volume { get; set; }
        public int SchematicQuantity { get; set; }
        public ListChange<CraftingValue>[] CraftingValues { get; set; }

        public ManufactureSchematicObjectDeltaMessage3()
        {
        }

        public ManufactureSchematicObjectDeltaMessage3(Message message, bool parseFromData = false) : base(message, parseFromData)
        {
        }

        public ManufactureSchematicObjectDeltaMessage3(byte[] Data, int Size, bool parseFromData = false) : base(Data, Size, parseFromData)
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
                        Complexity = ReadFloat();
                        break;
                    case 0x01:
                        STFFile = ReadString(Encoding.ASCII);
                        SetReadIntForwardBy(1);
                        STFName = ReadString(Encoding.ASCII);
                        break;
                    case 0x02:
                        CustomName = ReadString(Encoding.UTF8);
                        break;
                    case 0x03:
                        Volume = ReadInt32();
                        break;
                    case 0x04:
                        SchematicQuantity = ReadInt32();
                        break;
                    case 0x05:
                        CraftingValues = ReadNonIndexListChanges<CraftingValue>();
                        break;
                }

            }

            return true;
        }
    }
}
