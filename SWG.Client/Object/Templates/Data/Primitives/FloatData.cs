using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxv.Swg.Common.Files;
using SWG.Client.Utils;

namespace SWG.Client.Object.Templates.Data
{
    public class FloatData : TemplateBase<float>
    {

        public float Min { get; set; }
        public float Max { get; set; }


        public FloatData() : base(DataTypes.Float)
        {

        }

        public FloatData(byte[] Data, ref int Offset) : base(Data, ref Offset, DataTypes.Float)
        {

        }

        public FloatData(IFFFile.Node Source, ref int Offset) : base(Source, ref Offset, DataTypes.Float)
        {

        }


        public override bool Parse(IFFFile.Node Source, ref int offset)
        {
            return Parse(Source.Data, ref offset);
        }

        protected override bool InternalParseValue(byte[] Data, ref int offset, byte type)
        {
            if (offset >= Data.Length)
            {
                return false;
            }

            byte secondaryType = Data.ReadByte(ref offset);

            if (type == 1 && secondaryType == 0x20)
            {
                Value = Data.ReadFloat(ref offset);

                return true;
            }
            else if (type == 3 && secondaryType == 0x20)
            {
                Min = Data.ReadFloat(ref offset);
                Max = Data.ReadFloat(ref offset);
                return true;
            }

            return false;
        }

    }
}
