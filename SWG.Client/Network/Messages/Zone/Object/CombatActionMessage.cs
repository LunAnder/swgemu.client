using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using SWG.Client.Network.Objects.Zone.Object;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone.Object
{
    [ObjectControllerMessage(0xCC)]
    public class CombatActionMessage : ObjectControllerMessage
    {
        public int ActionCrc { get; set; }
        public long AttackerId { get; set; }
        public long WeaponId { get; set; }
        public byte AttackerEndPosture { get; set; }
        public byte TrailsBitFlag { get; set; }
        public byte AttackerCombatSpecialMoveEffect { get; set; }

        public List<Defender> Defenders; 

        public CombatActionMessage()
        {
        }

        public CombatActionMessage(Message message, bool parseFromData = false)
            : base(message, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
            {
                return false;
            }

            ActionCrc = ReadInt32();
            AttackerId = ReadInt64();
            WeaponId = ReadInt64();
            AttackerEndPosture = ReadByte();
            TrailsBitFlag = ReadByte();
            AttackerCombatSpecialMoveEffect = ReadByte();

            Defenders = ReadList<Defender>(() => ReadInt16());

            return true;
        }
    }
}
