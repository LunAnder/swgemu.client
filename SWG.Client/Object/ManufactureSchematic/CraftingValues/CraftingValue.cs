using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Messages;

namespace SWG.Client.Object.ManufactureSchematic.CraftingValues
{
    public class CraftingValue : IDeserializableFromMessage<CraftingValue>
    {
        public string STFFile { get; set; }
        public string STFName { get; set; }
        public float Value { get; set; }

        public CraftingValue Deserialize(Network.IDataContainerRead DataContainer)
        {
            STFFile = DataContainer.ReadString(Encoding.ASCII);
            DataContainer.SetReadIntForwardBy(1);
            STFName = DataContainer.ReadString(Encoding.ASCII);
            Value = DataContainer.ReadFloat();
            return this;
        }
    }
}
