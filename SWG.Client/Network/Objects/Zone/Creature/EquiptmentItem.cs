using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SWG.Client.Network.Messages;



namespace SWG.Client.Network.Objects.Zone.Creature
{
    public class EquiptmentItem : IDeserializableFromMessage<EquiptmentItem>
    {
        public string CustomizationString { get; set;  }
        public int ContainmentType { get; set; }
        public long ObjectId { get; set; }
        public int TemplateCRC { get; set; }


        public EquiptmentItem Deserialize(IDataContainerRead DataContainer)
        {
            return new EquiptmentItem
                {
                        CustomizationString = DataContainer.ReadString(Encoding.ASCII),
                        ContainmentType = DataContainer.ReadInt32(),
                        ObjectId = DataContainer.ReadInt64(),
                        TemplateCRC = DataContainer.ReadInt32(),
                };
        }
    }
}
