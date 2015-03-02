using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network.Messages.Zone.Creature
{
    public class CreatureObjectMessage1 : BaselineMessage
    {

        public int BankCredits { get; set; }
        public int CashCredits { get; set; }

        public int[] HAM { get; set; }

        public string[] Skills { get; set; }

        public CreatureObjectMessage1(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
                return false;

            BankCredits = ReadInt32();
            CashCredits = ReadInt32();

            HAM = ReadList(ReadInt32);
            Skills = ReadList(() => ReadString(Encoding.ASCII));


            return true;
        }
    }
}
