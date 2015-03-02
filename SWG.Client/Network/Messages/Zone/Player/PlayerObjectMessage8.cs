using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network;
using SWG.Client.Network.Messages;
using SWG.Client.Network.Objects.Zone.Player;



namespace SWG.Client.Network.Messages.Zone.Player
{
    public class PlayerObjectMessage8 : BaselineMessage
    {

        public Experience[] PlayerExperience { get; set; }
        public Waypoint[] Waypoints { get; set; }
        public int CurrentForcePower { get; set; }
        public int MaxForcePower { get; set; }
        public byte[] CurrentFSQuestMask { get; set; }
        public byte[] CommpletedFSQuestMask { get; set; }
        public QuestJournalItem[] QuestJournalItems { get; set; }


        public PlayerObjectMessage8(byte[] Data, int Size = 0, bool parseFromData = false)
            : base(Data, Size, parseFromData)
        {
        }

        public PlayerObjectMessage8(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
                return false;

            PlayerExperience = ReadList<Experience>(true, true);

            Waypoints = ReadList<Waypoint>(true, true);

            CurrentForcePower = ReadInt32();
            MaxForcePower = ReadInt32();
            CurrentFSQuestMask = ReadList(ReadByte);
            CommpletedFSQuestMask = ReadList(ReadByte);
            QuestJournalItems = ReadList<QuestJournalItem>(true, true);

            return true;
        }
    }
}
