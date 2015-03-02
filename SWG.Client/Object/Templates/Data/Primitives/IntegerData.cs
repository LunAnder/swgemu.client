using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxv.Swg.Common.Files;
using SWG.Client.Utils;

namespace SWG.Client.Object.Templates.Data
{
    public class IntegerData : TemplateBase<int>
    {

        public IntegerData() : base(DataTypes.Int)
        {

        }

        public IntegerData(IFFFile.Node Source, ref int Offset) : base(Source, ref Offset, DataTypes.Int)
        {

        }

        public override bool Parse(IFFFile.Node Source, ref int offset)
        {
            byte readCase = Source.Data.ReadByte(ref offset);
            byte readCase2 = Source.Data.ReadByte(ref offset);

            if (readCase == 1 && readCase2 == 0x20)
            {
                Value = Source.Data.ReadInt32(ref offset);
                return true;
            }

            return false;
        }
    }
}
