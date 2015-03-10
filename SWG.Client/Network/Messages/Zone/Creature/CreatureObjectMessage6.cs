using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SWG.Client.Network.Objects.Zone.Creature;
using SWG.Client.Utils;


namespace SWG.Client.Network.Messages.Zone.Creature
{
    [RegisterBaselineMessage(MessageOp.CREO, 0x06)]
    public class CreatureObjectMessage6 : Tangible.TangibleObjectMessage6
    {
        public ushort DifficultyCon { get; set; }
        public string CurrentAnimation { get; set; }
        public string MoodAnimation { get; set; }
        public long WeaponId { get; set; }
        public long GroupId { get; set; }
        public long InviteSenderId { get; set; }
        public long InviteCounter { get; set; }
        public int GuildId { get; set; }
        public long TargetId { get; set; }
        public byte MoodId { get; set; }
        public int PerformanceStartTime { get; set; }
        public int PerformanceId { get; set; }
        public int[] CurrentHAM { get; set; }
        public int[] MaxHAM { get; set; }
        public EquiptmentItem[] EquiptmentItems { get; set; }
        public string SetObjectTemplateString { get; set; }
        public byte StationaryFlag { get; set; }

        public CreatureObjectMessage6() { }


        public CreatureObjectMessage6(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
                return false;
            DifficultyCon = ReadUInt16();
            CurrentAnimation = ReadString(Encoding.ASCII);
            MoodAnimation = ReadString(Encoding.ASCII);
            WeaponId = ReadInt64();
            GroupId = ReadInt64();
            InviteSenderId = ReadInt64();
            InviteCounter = ReadInt64();
            GuildId = ReadInt32();
            TargetId = ReadInt64();
            MoodId = ReadByte();
            PerformanceStartTime = ReadInt32();
            PerformanceId = ReadInt32();

            CurrentHAM = ReadList(ReadInt32);
            MaxHAM = ReadList(ReadInt32);

            EquiptmentItems = ReadList<EquiptmentItem>();
            SetObjectTemplateString = ReadString(Encoding.ASCII);
            StationaryFlag = ReadByte();
            
            return true;
        }
    }
}
