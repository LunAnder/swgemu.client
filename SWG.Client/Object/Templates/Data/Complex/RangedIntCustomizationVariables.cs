using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Object.Templates.Data.Complex
{
    public class RangedIntCustomizationVariables : ComplexVectorData<RangedIntCustomizationVariable>
    {
        protected override string NodeName
        {
            get
            {
                return "RICV";
            }
        }
    }
}
