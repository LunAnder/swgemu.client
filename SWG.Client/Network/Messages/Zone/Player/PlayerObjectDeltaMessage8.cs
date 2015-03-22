using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network;
using SWG.Client.Network.Messages;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Network.Objects.Zone.Player;
using SWG.Client.Utils;
using SWG.Client.Utils.Attribute;


namespace SWG.Client.Network.Messages.Zone.Player
{
    [RegisterDeltaMessage(MessageOp.PLAY, 0x08)]
    public class PlayerObjectDeltaMessage8 : DeltaMessage
    {
        public ListChange<Experience>[] PlayerExperience { get; set; }
        public ListChange<Waypoint>[] Waypoints { get; set; }
        public int? CurrentForcePower { get; set; }
        public int? MaxForcePower { get; set; }
        public byte[] CurrentFSQuestMask { get; set; }
        public byte[] CommpletedFSQuestMask { get; set; }
        public ListChange<QuestJournalItem>[] QuestJournalItems { get; set; }

        public PlayerObjectDeltaMessage8() { }

        public PlayerObjectDeltaMessage8(Message message, bool parseFromData = false)
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
                        PlayerExperience = ReadNonIndexListChanges<Experience>();
                        break;
                    case 0x01:
                        Waypoints = ReadNonIndexListChanges<Waypoint>();
                        break;
                    case 0x02:
                        CurrentForcePower = ReadInt32();
                        break;
                    case  0x03:
                        MaxForcePower = ReadInt32();
                        break;
                    case 0x04:
                        CurrentFSQuestMask = ReadList(ReadByte);
                        break;
                    case 0x05:
                        CommpletedFSQuestMask = ReadList(ReadByte);
                        break;
                    case 0x06:
                        QuestJournalItems = ReadNonIndexListChanges<QuestJournalItem>();
                        break;
                }
            }


            return true;
        }
    }
}
