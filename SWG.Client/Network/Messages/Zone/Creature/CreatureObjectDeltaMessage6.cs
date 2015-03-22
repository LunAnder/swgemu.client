using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Network.Objects.Zone.Creature;
using SWG.Client.Utils;
using SWG.Client.Utils.Attribute;


namespace SWG.Client.Network.Messages.Zone.Creature
{
    [RegisterDeltaMessage(MessageOp.CREO, 0x06)]
    public class CreatureObjectDeltaMessage6 : DeltaMessage
    {
        public ushort? DifficultyCon { get; set; }
        public string CurrentAnimation { get; set; }
        public string MoodAnimation { get; set; }
        public long? WeaponId { get; set; }
        public long? GroupId { get; set; }
        public long? InviteSenderId { get; set; }
        public long? InviteCounter { get; set; }
        public int? GuildId { get; set; }
        public long? TargetId { get; set; }
        public byte? MoodId { get; set; }
        public int? PerformanceStartTime { get; set; }
        public int? PerformanceId { get; set; }
        public ListChange<int>[] CurrentHAM { get; set; }
        public ListChange<int>[] MaxHAM { get; set; }
        public ListChange<EquiptmentItem>[] EquiptmentItems { get; set; }
        public string SetObjectTemplateString { get; set; }
        public byte? StationaryFlag { get; set; }

        public CreatureObjectDeltaMessage6() { }

        public CreatureObjectDeltaMessage6(Message message, bool parseFromData = false)
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
                    case 0x02:
                        DifficultyCon = ReadUInt16();
                        break;
                    case 0x03:
                        CurrentAnimation = ReadString(Encoding.ASCII);
                        break;
                    case 0x04:
                        MoodAnimation = ReadString(Encoding.ASCII);
                        break;
                    case 0x05:
                        WeaponId = ReadInt64();
                        break;
                    case 0x06:
                        GroupId = ReadInt64();
                        break;
                    case 0x07:
                        InviteSenderId = ReadInt64();
                        InviteCounter = ReadInt64();
                        break;
                    case 0x08:
                        GuildId = ReadInt32();
                        break;
                    case  0x09:
                        TargetId = ReadInt64();
                        break;
                    case 0x0A:
                        MoodId = ReadByte();
                        break;
                    case 0x0B:
                        PerformanceStartTime = ReadInt32();
                        break;
                    case 0x0C:
                        PerformanceId = ReadInt32();
                        break;
                    case 0x0D:
                        CurrentHAM = ReadIntIndexedListChanges();
                        break;
                    case 0x0E:
                        MaxHAM = ReadIntIndexedListChanges();
                        break;
                    case 0x0F:
                        EquiptmentItems = ReadIndexedListChanges<EquiptmentItem>();
                        break;
                    case 0x10:
                        SetObjectTemplateString = ReadString(Encoding.ASCII);
                        break;
                    case 0x11:
                        StationaryFlag = ReadByte();
                        break;
                }
            }

            return true;
        }
    }
}
