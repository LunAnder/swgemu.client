using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SWG.Client.Network.Messages;



namespace SWG.Client.Network.Objects.Zone.Creature
{
    public class GroupMissionCriticalObject : IDeserializableFromMessage<GroupMissionCriticalObject>
    {
        public long MissionOwnerId { get; set; }
        public long MissionCriticalOjbectId { get; set; }

        public GroupMissionCriticalObject Deserialize(IDataContainerRead DataContainer)
        {
            return new GroupMissionCriticalObject
                {
                    MissionOwnerId = DataContainer.ReadInt64(),
                    MissionCriticalOjbectId = DataContainer.ReadInt64(),
                };
        }
    }
}
