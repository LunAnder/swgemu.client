using SWG.Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxv.Swg.Common.Files;

namespace SWG.Client.Object.Templates.Data.Complex
{
    public class PaletteColorCustomizationVariable : RangedIntCustomizationVariable
    {

        public StringData VariableName;
        public StringData PalettePathName;

        public PaletteColorCustomizationVariable()
        {
            Type = DataTypes.PaletteColoursCustomization;
        }

        public PaletteColorCustomizationVariable(int defaulIndex, string fileName) 
            : this()
        {
            DefaultValue = new IntegerData { Value = defaulIndex };
            PalettePathName = new StringData { Value = fileName };
        }

        public PaletteColorCustomizationVariable(IFFFile.Node Source, ref int Offset) 
            : this()
        {
            Parse(Source, ref Offset);
        }

        public override bool Parse(IFFFile.Node Source, ref int offset)
        {
            var toParse = Source.FindSiblingNodes("PCNT").ToList();
            var pcnt = toParse.First(cur => cur.Type == "PCNT");
            if (pcnt == null)
            {
                return false;
            }

            var idx = 0;
            var size = pcnt.Data.ReadInt32(ref idx);

            if(toParse.Count != size -1)
            {
                return false;
            }

            foreach (var node in toParse.Where(cur => cur.Type == "XXXX"))
            {
                int readIdx = 0;
                string varName = node.Data.ReadAsciiString(ref readIdx);
                switch (varName)
                {
                    case "variableName":
                        VariableName = new StringData(node, ref readIdx);
                        break;
                    case "palettePathName":
                        PalettePathName = new StringData(node, ref readIdx);
                        break;
                    case "defaultPaletteIndex":
                        DefaultValue = new IntegerData(node, ref readIdx);
                        break;
                    default:
                        break;
                }
            }

            return true;

        }

        public override bool Parse(byte[] Data, ref int offset)
        {
            throw new NotImplementedException();
        }

        
    }
}
