using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SWG.Client.Network.Messages;
using SWG.Client.Network.Messages.Base;


namespace SWG.Client.Network.Objects.Zone.Creature
{
    public class SkillModifier : IDeserializableFromMessage<SkillModifier>
    {
        public byte LeftoverDelta { get; set; }
        public string SkillModString { get; set; }
        public int BaseValue { get; set; }
        public int Modifier { get; set; }

        public SkillModifier Deserialize(IDataContainerRead DataContainer)
        {
            return new SkillModifier
                {
                        SkillModString = DataContainer.ReadString(Encoding.ASCII),
                        BaseValue = DataContainer.ReadInt32(),
                        Modifier = DataContainer.ReadInt32()
                };
        }
    }
}
