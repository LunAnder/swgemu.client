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
    [RegisterDeltaMessage(MessageOp.CREO, 0x04)]
    public class CreatureObjectDeltaMessage4 : DeltaMessage
    {
        public float? AccelerationMultiplierBase { get; set; }
        public float? AccelerationMultiplierMod { get; set; }
        public ListChange<int>[] HAMEncumberance { get; set; }
        public ListChange<SkillModifier>[] SkillModifiers { get; set; }
        public float? SpeedMultiplierBase { get; set; }
        public float? SpeedMultiplierMod { get; set; }
        public long? ListenToObjectId { get; set; }
        public float? RunSpeedBase { get; set; }
        public float? RunSpeedMod { get; set; }
        public float? SlopeAngle { get; set; }
        public float? SlopePercent { get; set; }
        public float? TurnRadius { get; set; }
        public float? WalkSpeed { get; set; }
        public float? WaterModPercent { get; set; }
        public ListChange<GroupMissionCriticalObject>[] GroupMissionCriticalObjects { get; set; }

        public CreatureObjectDeltaMessage4() { }

        public CreatureObjectDeltaMessage4(Message message, bool parseFromData = false)
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
                    case 0x00:
                        AccelerationMultiplierBase = ReadFloat();
                        break;
                    case 0x01:
                        AccelerationMultiplierMod = ReadFloat();
                        break;
                    case 0x02:
                        HAMEncumberance = ReadIntIndexedListChanges();
                        break;
                    case 0x03:
                        SkillModifiers =
                                ReadNonIndexListChanges<SkillModifier>();
                        break;
                    case 0x04:
                        SpeedMultiplierBase = ReadFloat();
                        break;
                    case 0x05:
                        SpeedMultiplierMod = ReadFloat();
                        break;
                    case 0x06:
                        ListenToObjectId = ReadInt64();
                        break;
                    case 0x07:
                        RunSpeedBase = ReadFloat();
                        break;
                    case 0x08:
                        SlopeAngle = ReadFloat();
                        break;
                    case 0x09:
                        SlopePercent = ReadFloat();
                        break;
                    case 0x0A:
                        TurnRadius = ReadFloat();
                        break;
                    case 0x0B:
                        WalkSpeed = ReadFloat();
                        break;
                    case 0x0C:
                        WaterModPercent = ReadFloat();
                        break;
                    case 0x0D:
                        GroupMissionCriticalObjects = ReadNonIndexListChanges<GroupMissionCriticalObject>();
                        break;
                }
            }



            return true;
        }
    }
}
