using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SWG.Client.Network.Messages;



namespace SWG.Client.Network.Objects.Zone.Player
{
    public class Experience : IDeserializableFromMessage<Experience>
    {
        public string Type { get; set; }
        public int Value { get; set; }


        public Experience Deserialize(IDataContainerRead DataContainer)
        {
            return new Experience
                {
                        Type = DataContainer.ReadString(Encoding.ASCII),
                        Value = DataContainer.ReadInt32(),
                };
        }
    }
}
