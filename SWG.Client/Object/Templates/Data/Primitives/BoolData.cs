using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxv.Swg.Common.Files;
using SWG.Client.Utils;

namespace SWG.Client.Object.Templates.Data
{
    public class BoolData : TemplateBase<bool>
    {

        public BoolData() : base(DataTypes.Bool)
        {

        }

        public BoolData(IFFFile.Node Source, ref int Offset) : base(Source, ref Offset, DataTypes.Bool)
        {

        }

        public BoolData(byte[] Data, ref int Offset) : base(Data, ref Offset, DataTypes.Bool)
        {

        }

        protected override bool InternalParseValue(byte[] Data, ref int offset, byte type)
        {
            if(offset >= Data.Length)
            {
                return false;
            }

            if (type == 1)
            {
                Value = Data.ReadByte(ref offset) == 1;
                return true;
            }

            return false;
        }

        public override bool Parse(IFFFile.Node Source, ref int offset)
        {
            return Parse(Source.Data, ref offset);
        }

        public static implicit operator BoolData(bool Value)
        {
            return new BoolData
            {
                Value = Value
            };
        }

        public static implicit operator bool(BoolData Data)
        {
            if(Data == null || !Data.HasValue)
            {
                throw new ArgumentNullException("Boolean Data does not contain a value");
            }

            return Data.Value;
        }
    }
}
