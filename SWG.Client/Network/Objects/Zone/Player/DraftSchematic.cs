using SWG.Client.Network.Messages;
using SWG.Client.Network.Messages.Base;


namespace SWG.Client.Network.Objects.Zone.Player
{
    public class DraftSchematic : IDeserializableFromMessage<DraftSchematic>
    {
        public int ServerSchematicCRC { get; set; }
        public int SchematicCRC { get; set; }


        public DraftSchematic Deserialize(IDataContainerRead DataContainer)
        {
            return new DraftSchematic
                {
                        ServerSchematicCRC = DataContainer.ReadInt32(),
                        SchematicCRC = DataContainer.ReadInt32(),
                };
        }
    }
}
