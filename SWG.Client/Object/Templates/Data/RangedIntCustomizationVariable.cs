using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxv.Swg.Common.Files;
using SWG.Client.Utils;

namespace SWG.Client.Object.Templates.Data
{
    public class RangedIntCustomizationVariable : DataBase
    {

        public StringData VariableName { get; set; }
        public IntegerData MinValueInclusive { get; set; }
        public IntegerData DefaultValue { get; set; }
        public IntegerData MaxValueExclusive { get; set; }

        public RangedIntCustomizationVariable(IFFFile.Node Source, ref int Offset) : base(Source, ref Offset, DataTypes.RangedIntCustomizationVariable)
        {

        }

        public RangedIntCustomizationVariable() : base(DataTypes.RangedIntCustomizationVariable)
        {

        }


        public override bool Parse(IFFFile.Node Source, ref int offset)
        {
            int numVars = Source.Data.ReadInt32(ref offset);
            
            if(numVars != Source.Children.Count())
            {
                throw new InvalidOperationException(string.Format("count in data stream({0}) does not match the number of nodes ({1})", numVars, Source.Children.Count()));
            }

            foreach (var item in Source.Children.Where(cur => cur.Type == "XXXX"))
            {
                var readOffset = 0;
                var varName = item.Data.ReadAsciiString(ref readOffset);

                switch (varName)
                {
                    case "variableName":
                        VariableName = new StringData(item, ref readOffset);
                        break;
                    case "minValueInclusive":
                        MinValueInclusive = new IntegerData(item, ref readOffset);
                        break;
                    case "defaultValue":
                        DefaultValue = new IntegerData(item, ref readOffset);
                        break;
                    case "maxValueExclusive":
                        MaxValueExclusive = new IntegerData(item, ref readOffset);
                        break;
                }
            }

            return true;
        }
    }
}
