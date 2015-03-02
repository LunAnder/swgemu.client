using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Object.Templates.Data
{
    public enum DataTypes : uint
    {
        Bool = 0x01,
        Float = 0x02,
        Int = 0x04,
        String = 0x08,
        StringId = 0x10,
        Vector = 0x12,
        DynamicVariable = 0x014,
        TriggerVolume = 0x16,
        PaletteColoursCustomization = 0x18,
        RangedIntCustomizationVariable = 0x20,
        CustomizationVariableMap = 0x22,
    }
}
