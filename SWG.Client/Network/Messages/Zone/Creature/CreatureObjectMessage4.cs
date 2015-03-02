using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SWG.Client.Network.Objects.Zone.Creature;



namespace SWG.Client.Network.Messages.Zone.Creature
{
    public class CreatureObjectMessage4 : BaselineMessage
    {
        public float AccelerationMultiplierBase { get; set; }
        public float AccelerationMultiplierMod { get; set; }
        public int[] HAMEncumberance { get; set; }
        public SkillModifier[] SkillModifiers { get; set; }
        public float SpeedMultiplierBase { get; set; }
        public float SpeedMultiplierMod { get; set; }
        public long ListenToObjectId { get; set; }
        public float RunSpeedBase { get; set; }
        public float RunSpeedMod { get; set; }
        public float SlopeAngle { get; set; }
        public float SlopePercent { get; set; }
        public float TurnRadius { get; set; }
        public float WalkSpeed { get; set; }
        public float WaterModPercent { get; set; }
        public GroupMissionCriticalObject[] GroupMissionCriticalObjects { get; set; }
        

        public CreatureObjectMessage4(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
                return false;

            AccelerationMultiplierBase = ReadFloat();
            AccelerationMultiplierMod = ReadFloat();

            HAMEncumberance = ReadList(ReadInt32);

            SkillModifiers = ReadList<SkillModifier>();

            SpeedMultiplierBase = ReadFloat();
            SpeedMultiplierMod = ReadFloat();
            ListenToObjectId = ReadInt64();
            SlopeAngle = ReadFloat();
            SlopePercent = ReadFloat();
            TurnRadius = ReadFloat();
            WalkSpeed = ReadFloat();
            WaterModPercent = ReadFloat();

            GroupMissionCriticalObjects = ReadList<GroupMissionCriticalObject>();

            return true;
        }
    }



}
