using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Network.Objects;
using SWG.Client.Object.ManufactureSchematic.CraftingValues;
using SWG.Client.Utils;

namespace SWG.Client.Network.Messages.Zone.ManufactureSchematic
{
    [RegisterBaselineMessage(MessageOp.MSCO, 0x03)]
    public class ManufactureSchematicObjectMessage3 : BaselineMessage
    {
        public float Complexity { get; set; }
        public StringFile STFFile { get; set; }
        public string CustomName { get; set; }
        public int Volume { get; set; }
        public int SchematicQuantity { get; set; }
        public CraftingValue[] CraftingValues { get; set; }
        public string CreatorName { get; set; }
        public int Complexity2 { get; set; }
        public float SchematicDataSize { get; set; }

        public ManufactureSchematicObjectMessage3()
        {
        }

        public ManufactureSchematicObjectMessage3(byte[] Data, int Size, bool parseFromData = false) : base(Data, Size, parseFromData)
        {
        }

        public ManufactureSchematicObjectMessage3(Message message, bool parseFromData = false) : base(message, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
            {
                return false;
            }
            

            Complexity = ReadFloat();
            STFFile = new StringFile().Deserialize(this);
            CustomName = ReadString(Encoding.UTF8);
            Volume = ReadInt32();
            SchematicQuantity = ReadInt32();
            CraftingValues = ReadList<CraftingValue>(true,true);
            CreatorName = ReadString(Encoding.UTF8);
            Complexity2 = ReadInt32();
            SchematicDataSize = ReadFloat();
            return true;
        }
    }
}
