using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils;

namespace SWG.Client.Network.Messages.Zone.Creature
{
    [RegisterBaselineMessage(MessageOp.CREO,0x03)]
    public class CreatureObjectMessage3 : Tangible.TangibleObjectMessage3
    {

        public byte PostureId { get; set; }
        public byte FactionRank { get; set; }
        public long OwnerId { get; set; }
        public float HightScale { get; set; }
        public int BattleFatigue { get; set; }
        public long StatesBitmask { get; set; }

        public int[] HAMWounds { get; set; }

        public CreatureObjectMessage3() { }

        public CreatureObjectMessage3(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
                return false;

            PostureId = ReadByte();
            FactionRank = ReadByte();
            OwnerId = ReadInt64();
            HightScale = ReadFloat();
            BattleFatigue = ReadInt32();
            StatesBitmask = ReadInt64();
            HAMWounds = ReadList(ReadInt32);
            
            return true;
        }
    }
}
