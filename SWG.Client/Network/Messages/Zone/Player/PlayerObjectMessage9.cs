using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network;
using SWG.Client.Network.Messages;
using SWG.Client.Network.Objects.Zone.Creature;
using SWG.Client.Network.Objects.Zone.Player;
using SWG.Client.Utils;


namespace SWG.Client.Network.Messages.Zone.Player
{
    [RegisterBaselineMessage(MessageOp.PLAY, 0x09)]
    public class PlayerObjectMessage9 : BaselineMessage
    {
        public string[] Abilities { get; set; }
        public int ExperimentationFlag { get; set; }
        public int CraftingStage { get; set; }
        public long NearestCraftingStationId { get; set; }
        public DraftSchematic[] DraftSchematics { get; set; }
        public int ExperimentationPoints { get; set; }
        public int AccomplishmentCounter { get; set; }
        public string[] Friends { get; set; }
        public string[] IgnoreList { get; set; }
        public int CurrentLanguageId { get; set; }
        public int CurrentStomac { get; set; }
        public int MaxStomac { get; set; }
        public int CurrentDrink { get; set; }
        public int MaxDrink { get; set; }
        public int CurrentConsumable { get; set; }
        public int MaxConsumable { get; set; }
        public Waypoint[] UnusedWaypoints { get; set; }
        public int JediState { get; set; }

        public PlayerObjectMessage9() { }

        public PlayerObjectMessage9(byte[] Data, int Size = 0, bool parseFromData = false)
            : base(Data, Size, parseFromData)
        {
        }

        public PlayerObjectMessage9(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
                return false;

            Abilities = ReadList(() => ReadString(Encoding.ASCII));
            ExperimentationFlag = ReadInt32();
            CraftingStage = ReadInt32();
            NearestCraftingStationId = ReadInt64();
            DraftSchematics = ReadList<DraftSchematic>();
            ExperimentationPoints = ReadInt32();
            AccomplishmentCounter = ReadInt32();
            Friends = ReadList(() => ReadString(Encoding.ASCII));
            IgnoreList = ReadList(() => ReadString(Encoding.ASCII));
            CurrentLanguageId = ReadInt32();
            CurrentStomac = ReadInt32();
            MaxStomac = ReadInt32();
            CurrentDrink = ReadInt32();
            MaxDrink = ReadInt32();
            CurrentConsumable = ReadInt32();
            MaxConsumable = ReadInt32();
            UnusedWaypoints = ReadList<Waypoint>(true, true);
            JediState = ReadInt32();

            return true;
        }
    }
}
