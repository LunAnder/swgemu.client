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

        public override bool Parse(IFFFile.Node Source, ref int offset)
        {
            int readCase = Source.Data.ReadByte(ref offset);
            if(readCase == 1)
            {
                Value = Source.Data.ReadAsciiString(ref offset);
                return true;
            }

            return false;
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
    }
}
