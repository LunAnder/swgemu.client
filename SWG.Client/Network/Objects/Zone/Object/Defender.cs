using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Messages;

namespace SWG.Client.Network.Objects.Zone.Object
{
    public class Defender : IDeserializableFromMessage<Defender>
    {
        public long DefenderObjectId { get; set; }
        public byte DefenderEndPosture { get; set; }
        public byte HitType { get; set; }
        public byte DefenderCombatSpecialMoveEffect { get; set; }

        public Defender Deserialize(IDataContainerRead DataContainer)
        {
            DefenderObjectId = DataContainer.ReadInt64();
            DefenderEndPosture = DataContainer.ReadByte();
            HitType = DataContainer.ReadByte();
            DefenderCombatSpecialMoveEffect = DataContainer.ReadByte();
            return this;
        }
    }
}
