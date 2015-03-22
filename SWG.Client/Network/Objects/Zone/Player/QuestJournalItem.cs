using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SWG.Client.Network.Messages;
using SWG.Client.Network.Messages.Base;


namespace SWG.Client.Network.Objects.Zone.Player
{
    public class QuestJournalItem : IDeserializableFromMessage<QuestJournalItem>
    {
        public int QuestCRC { get; set; }
        public long OwnerId { get; set; }
        public short ActiveStepBitmask { get; set; }
        public short CompeltedStepBitmask { get; set; }
        public byte CompeltedFlag { get; set; }
        public int QuestCounter { get; set; }


        public QuestJournalItem Deserialize(IDataContainerRead DataContainer)
        {
            return new QuestJournalItem
                {
                        QuestCRC = DataContainer.ReadInt32(),
                        OwnerId = DataContainer.ReadInt64(),
                        ActiveStepBitmask = DataContainer.ReadInt16(),
                        CompeltedStepBitmask = DataContainer.ReadInt16(),
                        CompeltedFlag = DataContainer.ReadByte(),
                        QuestCounter = DataContainer.ReadInt32(),
                };
        }
    }
}
