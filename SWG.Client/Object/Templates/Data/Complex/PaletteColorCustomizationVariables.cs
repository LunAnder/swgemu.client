using SWG.Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxv.Swg.Common.Files;

namespace SWG.Client.Object.Templates.Data.Complex
{
    public class PaletteColorCustomizationVariables : ComplexVectorData<PaletteColorCustomizationVariable>
    {
        protected override string NodeName
        {
            get
            {
                return "PCCV";
            }
        }
    }
}
