using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Objects.Zone.Player;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone.Mission
{
    [RegisterDeltaMessage(MessageOp.MISO, 0x03)]
    public class MissionObjectDeltaMessage3 : DeltaMessage
    {
        public float Complexity { get; set; }
        public string STFFile { get; set; }
        public string STFName { get; set; }
        public string CustomName { get; set; }
        public int Volume { get; set; }
        public int DifficultyLevel { get; set; }
        public float StartX { get; set; }
        public float StartZ { get; set; }
        public float StartY { get; set; }
        public long StartObjectId { get; set; }
        public int StartPlanetCrc { get; set; }
        public string CreatorName { get; set; }
        public int CreditsReward { get; set; }
        public float DestinationX { get; set; }
        public float DestinationZ { get; set; }
        public float DestinationY { get; set; }
        public long DestinationObjectId { get; set; }
        public int DestinationPlanetCrc { get; set; }
        public int TargetObjectIffCrc { get; set; }
        public string MissionDescriptionSTFFile { get; set; }
        public string MissionDescriptionSTFName { get; set; }
        public string MissionTitleSTFFile { get; set; }
        public string MissionTitleSTFName { get; set; }
        public int RepeatCounter { get; set; }
        public int MissionTypeCrc { get; set; }
        public string TargetName { get; set; }
        public Waypoint Waypoint { get; set; }


        public MissionObjectDeltaMessage3()
        {
        }

        public MissionObjectDeltaMessage3(byte[] Data, int Size, bool parseFromData = false) : base(Data, Size, parseFromData)
        {
        }

        public MissionObjectDeltaMessage3(Message message, bool parseFromData = false) : base(message, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
            {
                return false;
            }

            for (int i = 0; i < UpdateCount; i++)
            {
                var updateId = ReadInt16();
                switch (updateId)
                {
                    case 0x00:
                        Complexity = ReadFloat();
                        break;
                    case 0x01:
                        STFFile = ReadString(Encoding.ASCII);
                        SetReadIntForwardBy(1);
                        STFName = ReadString(Encoding.ASCII);
                        break;
                    case 0x02:
                        CustomName = ReadString(Encoding.UTF8);
                        break;
                    case 0x03:
                        Volume = ReadInt32();
                        break;
                    case 0x04:
                        SetReadIntForwardBy(1);
                        break;
                    case 0x05:
                        DifficultyLevel = ReadInt32();
                        break;
                    case 0x06:
                        StartX = ReadFloat();
                        StartZ = ReadFloat();
                        StartY = ReadFloat();
                        StartObjectId = ReadInt64();
                        StartPlanetCrc = ReadInt32();
                        break;
                    case 0x07:
                        CreatorName = ReadString(Encoding.UTF8);
                        break;
                    case 0x08:
                        CreditsReward = ReadInt32();
                        break;
                    case 0x09:
                        DestinationX = ReadFloat();
                        DestinationZ = ReadFloat();
                        DestinationY = ReadFloat();
                        DestinationObjectId = ReadInt64();
                        DestinationPlanetCrc = ReadInt32();
                        break;
                    case 0x0A:
                        TargetObjectIffCrc = ReadInt32();
                        break;
                    case 0x0B:
                        MissionDescriptionSTFFile = ReadString(Encoding.ASCII);
                        SetReadIntForwardBy(1);
                        MissionDescriptionSTFName = ReadString(Encoding.ASCII);
                        break;
                    case 0x0C:
                        MissionTitleSTFFile = ReadString(Encoding.ASCII);
                        SetReadIntForwardBy(1);
                        MissionTitleSTFName = ReadString(Encoding.ASCII);
                        break;
                    case 0x0D:
                        RepeatCounter = ReadInt32();
                        break;
                    case 0x0E:
                        MissionTypeCrc = ReadInt32();
                        break;
                    case 0x0F:
                        TargetName = ReadString(Encoding.ASCII);
                        break;
                    case 0x10:
                        Waypoint = (new Waypoint()).Deserialize(this);
                        break;
                }

            }

            return true;
        }
    }
}
