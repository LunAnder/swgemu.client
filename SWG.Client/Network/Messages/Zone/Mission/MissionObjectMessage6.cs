using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Utils;

namespace SWG.Client.Network.Messages.Zone.Mission
{
    [RegisterBaselineMessage(MessageOp.MISO, 0x06)]
    public class MissionObjectMessage6 : BaselineMessage
    {
        public int ServerId { get; set; }

        public MissionObjectMessage6()
        {
        }

        public MissionObjectMessage6(byte[] Data, int Size, bool parseFromData = false) : base(Data, Size, parseFromData)
        {
        }

        public MissionObjectMessage6(Message message, bool parseFromData = false) : base(message, parseFromData)
        {
        }


        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
            {
                return false;
            }

            ServerId = ReadInt32();

            return true;
        }
    }
}
