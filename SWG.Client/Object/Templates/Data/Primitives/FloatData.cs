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

        public FloatData(IFFFile.Node Source, ref int Offset) : base(Source, ref Offset, DataTypes.Float)
        {

        }


        public override bool Parse(IFFFile.Node Source, ref int offset)
        {
            byte readCase = Source.Data.ReadByte(ref offset);
            byte readCase2 = Source.Data.ReadByte(ref offset);

            if (readCase == 1 && readCase2 == 0x20)
            {
                Value = Source.Data.ReadFloat(ref offset);

                return true;
            }
            else if(readCase == 3 && readCase2 == 0x20)
            {
                Min = Source.Data.ReadFloat(ref offset);
                Max = Source.Data.ReadFloat(ref offset);
            }

            return false;
        }

    }
}
