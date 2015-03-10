using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network;
using SWG.Client.Network.Messages;
using SWG.Client.Network.Objects.Zone.Player;
using SWG.Client.Utils;


namespace SWG.Client.Network.Messages.Zone.Player
{
    [RegisterDeltaMessage(MessageOp.PLAY, 0x09)]
    public class PlayerObjectDeltaMessage9 : DeltaMessage
    {

        public ListChange<string>[] Abilities { get; set; }
        public int? ExperimentationFlag { get; set; }
        public int? CraftingStage { get; set; }
        public long? NearestCraftingStationId { get; set; }
        public ListChange<DraftSchematic>[] DraftSchematics { get; set; }
        public int? ExperimentationPoints { get; set; }
        public int? AccomplishmentCounter { get; set; }
        public ListChange<string>[] Friends { get; set; }
        public ListChange<string>[] IgnoreList { get; set; }
        public int? CurrentLanguageId { get; set; }
        public int? CurrentStomac { get; set; }
        public int? MaxStomac { get; set; }
        public int? CurrentDrink { get; set; }
        public int? MaxDrink { get; set; }
        public int? CurrentConsumable { get; set; }
        public int? MaxConsumable { get; set; }
        public ListChange<Waypoint>[] UnusedWaypoints { get; set; }
        public int? JediState { get; set; }

        public PlayerObjectDeltaMessage9() { }

        public PlayerObjectDeltaMessage9(Message message, bool parseFromData = false)
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
                        Abilities = ReadIndexedListChanges(() => ReadString(Encoding.ASCII));
                        break;
                    case 0x01:
                        ExperimentationFlag = ReadInt32();
                        break;
                    case 0x02:
                        CraftingStage = ReadInt32();
                        break;
                    case 0x03:
                        NearestCraftingStationId = ReadInt64();
                        break;
                    case 0x04:
                        DraftSchematics = ReadIndexedListChanges<DraftSchematic>();
                        break;
                    case 0x05:
                        ExperimentationFlag = ReadInt32();
                        break;
                    case 0x06:
                        AccomplishmentCounter = ReadInt32();
                        break;
                    case 0x07:
                        Friends = ReadIndexedListChanges(() => ReadString(Encoding.ASCII));
                        break;
                    case 0x08:
                        IgnoreList = ReadIndexedListChanges(() => ReadString(Encoding.ASCII));
                        break;
                    case 0x09:
                        CurrentLanguageId = ReadInt32();
                        break;
                    case 0x0A:
                        CurrentStomac = ReadInt32();
                        break;
                    case 0x0B:
                        MaxStomac = ReadInt32();
                        break;
                    case 0x0C:
                        CurrentDrink = ReadInt32();
                        break;
                    case 0x0D:
                        MaxDrink = ReadInt32();
                        break;
                    case 0x0E:
                        CurrentConsumable = ReadInt32();
                        break;
                    case 0x0F:
                        MaxConsumable = ReadInt32();
                        break;
                    case 0x10:
                        UnusedWaypoints = ReadNonIndexListChanges<Waypoint>();
                        break;
                    case 0x11:
                        JediState = ReadInt32();
                        break;
                }
            }
            
            return true;
        }
    }
}
