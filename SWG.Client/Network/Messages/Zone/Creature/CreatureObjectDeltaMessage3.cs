using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network.Messages.Zone.Creature
{
    public class CreatureObjectDeltaMessage3 : DeltaMessage
    {

        public byte? PostureId { get; set; }
        public byte? FactionRank { get; set; }
        public long? OwnerId { get; set; }
        public float? HightScale { get; set; }
        public int? BattleFatigue { get; set; }
        public long? StatesBitmask { get; set; }

        public ListChange<int>[] HAMWounds { get; set; }

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
                        /*HAMWounds = ReadListChanges(() =>
                                                        {
                                                            var operationType = ReadByte();
                                                            var change = new ListChange<int>();
                                                            switch (operationType)
                                                            {
                                                                case 0x00:
                                                                    change.Operation = ListChangeOperation.Remove;
                                                                    change.Index = ReadInt16();
                                                                    break;
                                                                case 0x01:
                                                                    change.Operation = ListChangeOperation.Add;
                                                                    change.Index = ReadInt16();
                                                                    change.Value = ReadInt32();
                                                                break;
                                                                case 0x02:
                                                                    change.Operation = ListChangeOperation.Change;
                                                                    change.Index = ReadInt16();
                                                                    change.Value = ReadInt32();
                                                                    break;
                                                                case 0x03:
                                                                    change.Operation = ListChangeOperation.ResetAll;
                                                                    var size = ReadInt16();
                                                                    change.Values = new int[size];
                                                                    for (int j = 0; j < size; j++)
                                                                    {
                                                                        change.Values[j] = ReadInt32();
                                                                    }
                                                                    break;
                                                                case 0x04:
                                                                    change.Operation = ListChangeOperation.ClearAll;
                                                                    break;
                                                            }
                                                            return change;
                                                        });*/
                        break;
                }
            }


            return true;
        }
    }
}
