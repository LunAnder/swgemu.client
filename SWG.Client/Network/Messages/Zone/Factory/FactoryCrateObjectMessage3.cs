using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Utils;

namespace SWG.Client.Network.Messages.Zone.Factory
{
    [RegisterBaselineMessage(MessageOp.FCYT, 0x03)]
    public class FactoryCrateObjectMessage3 : BaselineMessage
    {

        public float Complexity { get; set; }
        public string STFFile { get; set; }
        public string STFName { get; set; }
        public string CustomName { get; set; }
        public int Volume { get; set; }
        public string CustomizationString { get; set; }
        //public int Unknown1 { get; set; }
        //public int Unknown2 { get; set; }
        public int OptionsBitmask { get; set; }
        public int UseCount { get; set; }
        public int ConditionDamage { get; set; }
        public int MaxDamage { get; set; }
        public byte Unknown { get; set; }


        public FactoryCrateObjectMessage3()
        {
        }

        public FactoryCrateObjectMessage3(byte[] Data, int Size, bool parseFromData = false)
            : base(Data, Size, parseFromData)
        {
        }

        public FactoryCrateObjectMessage3(Message message, bool parseFromData = false) 
            : base(message, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
            {
                return false;
            }

            Complexity = ReadFloat();
            STFFile = ReadString(Encoding.ASCII);
            SetReadIntForwardBy(1); //spacer
            STFName = ReadString(Encoding.ASCII);
            CustomName = ReadString(Encoding.UTF8);
            Volume = ReadInt32();
            CustomizationString = ReadString(Encoding.ASCII);
            SetReadIntForwardBy(2); // 2 unknown ints
            OptionsBitmask = ReadInt32();
            UseCount = ReadInt32();
            ConditionDamage = ReadInt32();
            MaxDamage = ReadInt32();
            Unknown = ReadByte();


            return true;
        }
    }
}
