using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Utils;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone.Creature
{
    [RegisterDeltaMessage(MessageOp.CREO, 0x01)]
    public class CreatureObjectDeltaMessage1 : DeltaMessage
    {

        public int? BankCredits { get; set; }
        public int? CashCredits { get; set; }
        public ListChange<string>[] SkillList { get; set; }

        public CreatureObjectDeltaMessage1() { }

        public CreatureObjectDeltaMessage1(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
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
                        BankCredits = ReadInt32();
                        break;
                    case 0x01:
                        CashCredits = ReadInt32();
                        break;
                    case 0x03:
                        SkillList = ReadAsciiStringNonIndexListChanges();
                        break;
                }
            }


            return true;
        }
    }
}
