using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network;
using SWG.Client.Network.Messages;



namespace SWG.Client.Network.Messages.Zone.Player
{
    public class PlayerObjectDeltaMessage3 : DeltaMessage
    {
        public int[] FlagBitmasks { get; set; }
        public int[] ProfileBitmasks { get; set; }
        public string ProfessionTag { get; set; }
        public int? BornDate { get; set; }
        public int? TotalPlayTime { get; set; }
        public int? Unknown1 { get; set; }

        public PlayerObjectDeltaMessage3(Message message, bool parseFromData = false)
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
                    case 0x05:
                        FlagBitmasks = ReadList(ReadInt32,false);
                        break;
                    case 0x06:
                        ProfileBitmasks = ReadList(ReadInt32,false);
                        break;
                    case 0x07:
                        ProfessionTag = ReadString(Encoding.ASCII);
                        break;
                    case 0x08:
                        BornDate = ReadInt32();
                        break;
                    case 0x09: 
                        TotalPlayTime = ReadInt32();
                        break;

                }
            }

            return true;
        }
    }
}
