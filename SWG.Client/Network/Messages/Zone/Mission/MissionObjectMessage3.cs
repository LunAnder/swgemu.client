using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Network.Objects.Zone.Player;
using SWG.Client.Utils;

namespace SWG.Client.Network.Messages.Zone.Mission
{
    [RegisterBaselineMessage(MessageOp.MISO, 0x03)]
    public class MissionObjectMessage3 : BaselineMessage
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

        public MissionObjectMessage3()
        {
        }

        public MissionObjectMessage3(byte[] Data, int Size, bool parseFromData = false) 
            : base(Data, Size, parseFromData)
        {
        }

        public MissionObjectMessage3(Message message, bool parseFromData = false) 
            : base(message, parseFromData)
        {
        }


        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
            {
                return false;
            }

            Complexity = ReadFloat();
            STFFile = ReadString(Encoding.ASCII);
            SetReadIntForwardBy(1);
            STFName = ReadString(Encoding.ASCII);
            CustomName = ReadString(Encoding.UTF8);
            Volume = ReadInt32();
            SetReadIntForwardBy(1);
            DifficultyLevel = ReadInt32();
            StartX = ReadFloat();
            StartZ = ReadFloat();
            StartY = ReadFloat();
            StartObjectId = ReadInt64();
            StartPlanetCrc = ReadInt32();
            CreatorName = ReadString(Encoding.UTF8);
            CreditsReward = ReadInt32();
            DestinationX = ReadFloat();
            DestinationZ = ReadFloat();
            DestinationY = ReadFloat();
            DestinationObjectId = ReadInt64();
            DestinationPlanetCrc = ReadInt32();
            TargetObjectIffCrc = ReadInt32();
            MissionDescriptionSTFFile = ReadString(Encoding.ASCII);
            SetReadIntForwardBy(1);
            MissionDescriptionSTFName = ReadString(Encoding.ASCII);
            MissionTitleSTFFile = ReadString(Encoding.ASCII);
            SetReadIntForwardBy(1);
            MissionTitleSTFName = ReadString(Encoding.ASCII);
            RepeatCounter = ReadInt32();
            MissionTypeCrc = ReadInt32();
            TargetName = ReadString(Encoding.ASCII);
            Waypoint = (new Waypoint()).Deserialize(this);

            return true;
        }
    }
}
