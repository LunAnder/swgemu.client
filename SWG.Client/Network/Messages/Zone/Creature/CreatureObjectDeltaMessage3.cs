using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Utils;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone.Creature
{
    [RegisterDeltaMessage(MessageOp.CREO,0x03)]                    
    public class CreatureObjectDeltaMessage3 : DeltaMessage
    {

        public byte? PostureId { get; set; }
        public byte? FactionRank { get; set; }
        public long? OwnerId { get; set; }
        public float? HightScale { get; set; }
        public int? BattleFatigue { get; set; }
        public long? StatesBitmask { get; set; }

        public ListChange<int>[] HAMWounds { get; set; }

        public CreatureObjectDeltaMessage3() { }

        public CreatureObjectDeltaMessage3(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
                return false;

            for (int i = 0; i < UpdateCount; i++)
            {
                var updateId = ReadInt16();
                switch (updateId)
                {
                    case 0x0B:
                        PostureId = ReadByte();
                        break;
                    case 0x0C:
                        FactionRank = ReadByte();
                        break;
                    case 0x0D:
                        OwnerId = ReadInt64();
                        break;
                    case 0x0E:
                        HightScale = ReadFloat();
                        break;
                    case 0x0F:
                        BattleFatigue = ReadInt32();
                        break;
                    case 0x10:
                        StatesBitmask = ReadInt64();
                        break;
                    case 0x11:
                        HAMWounds = ReadIntIndexedListChanges();
                        break;
                }
            }


            return true;
        }
    }
}
