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

        public IntegerData(byte[] Data, ref int Offset) : base(Data, ref Offset, DataTypes.Int)
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

            var secondaryType = Data.ReadByte(ref offset);
            if (type != 01 || secondaryType != 0x20)
            {
                return false;
            }


            Value = Data.ReadInt32(ref offset);
            return true;
            
        }
    }
}
