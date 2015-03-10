using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxv.Swg.Common.Files;
using SWG.Client.Utils;

namespace SWG.Client.Object.Templates.Data
{
    public class StringData : TemplateBase<string>
    {

        public StringData() 
            : base(DataTypes.String)
        {
        }

        public StringData(IFFFile.Node Source, ref int Offset) : base(Source, ref Offset, DataTypes.String)
        {

        }

        public StringData(byte[] Data, ref int Offset) : base(Data, ref Offset, DataTypes.String)
        {

        }

        /*public override bool Parse(byte[] Data, ref int offset)
        {
            if(Data.Length >= offset)
            {
                return false;
            }

            int readCase = Data.ReadByte(ref offset);
            if (readCase == 1)
            {
                Value = Data.ReadAsciiString(ref offset);
                return true;
            }

            return false;
        }*/

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

            if (type != 01)
            {
                return false;
            }
            
            Value = Data.ReadAsciiString(ref offset);
            return true;
        }

        public static implicit operator StringData(string Value)
        {
            return new StringData
            {
                Value = Value,
            };
        }

        public static implicit operator string(StringData Data)
        {
            if(Data == null || !Data.HasValue)
            {
                return null;
            }

            return Data.Value;
        }

        public override string ToString()
        {
            return HasValue ? Value : base.ToString();
        }
    }
}
