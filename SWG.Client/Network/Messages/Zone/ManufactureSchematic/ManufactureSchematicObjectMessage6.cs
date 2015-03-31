using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Utils;

namespace SWG.Client.Network.Messages.Zone.ManufactureSchematic
{
    [RegisterBaselineMessage(MessageOp.MSCO, 0x06)]
    public class ManufactureSchematicObjectMessage6 : BaselineMessage
    {

        public int ServerId { get; set; }
        public string CustomizationString { get; set; }
        public string CustomizationModel { get; set; }
        public int PrototypeCrc { get; set; }
        public byte ActiveFlag { get; set; }
        public byte SlotCount { get; set; }

        public ManufactureSchematicObjectMessage6()
        {
        }

        public ManufactureSchematicObjectMessage6(byte[] Data, int Size, bool parseFromData = false) : base(Data, Size, parseFromData)
        {
        }

        public ManufactureSchematicObjectMessage6(Message message, bool parseFromData = false) : base(message, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
            {
                return false;
            }

            ServerId = ReadInt32();
            CustomizationString = ReadString(Encoding.ASCII);
            CustomizationModel = ReadString(Encoding.ASCII);
            PrototypeCrc = ReadInt32();
            ActiveFlag = ReadByte();
            SlotCount = ReadByte();
            return true;
        }
    }
}
