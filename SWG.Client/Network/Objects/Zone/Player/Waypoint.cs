using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SWG.Client.Network.Messages;



namespace SWG.Client.Network.Objects.Zone.Player
{
    public class Waypoint : IDeserializableFromMessage<Waypoint>
    {
        public long ObjectId { get; set; }
        public int CellId { get; set; }
        public float XCoord { get; set; }
        public float ZCoord { get; set; }
        public float YCoord { get; set; }
        public long LocationNetworkId { get; set; }
        public int PlanetCRC { get; set; }
        public string WaypointName { get; set; }
        public long WaypointObjectId { get; set; }
        public byte Colour { get; set; }
        public byte Active { get; set; }

        public Waypoint Deserialize(IDataContainerRead DataContainer)
        {
            return new Waypoint
                {
                        ObjectId = DataContainer.ReadInt64(),
                        CellId = DataContainer.ReadInt32(),
                        XCoord = DataContainer.ReadFloat(),
                        ZCoord = DataContainer.ReadFloat(),
                        YCoord = DataContainer.ReadFloat(),
                        LocationNetworkId = DataContainer.ReadInt64(),
                        PlanetCRC = DataContainer.ReadInt32(),
                        WaypointName = DataContainer.ReadString(Encoding.UTF8),
                        WaypointObjectId = DataContainer.ReadInt64(),
                        Colour = DataContainer.ReadByte(),
                        Active = DataContainer.ReadByte(),
                };
        }
    }
}
