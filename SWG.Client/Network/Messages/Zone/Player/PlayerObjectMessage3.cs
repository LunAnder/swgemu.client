using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network;
using SWG.Client.Network.Messages;



namespace SWG.Client.Network.Messages.Zone.Player
{
    public class PlayerObjectMessage3 : Intangible.IntangibleObjectMessage3
    {
        public int[] FlagBitmasks { get; set; }
        public int[] ProfileBitmasks { get; set; }
        public string ProfessionTag { get; set; }
        public int BornDate { get; set; }
        public int TotalPlayTime { get; set; }
        public int Unknown1 { get; set; }

        public PlayerObjectMessage3(byte[] Data, int Size = 0, bool parseFromData = false)
            : base(Data, Size, parseFromData)
        {
        }

        public PlayerObjectMessage3(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
                return false;

            FlagBitmasks = ReadList(ReadInt32, false);
            ProfileBitmasks = ReadList(ReadInt32, false);
            ProfessionTag = ReadString(Encoding.ASCII);
            BornDate = ReadInt32();
            TotalPlayTime = ReadInt32();
            Unknown1 = ReadInt32();

            return true;
        }
    }
}
