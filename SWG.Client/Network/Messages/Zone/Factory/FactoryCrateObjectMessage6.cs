using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Utils;

namespace SWG.Client.Network.Messages.Zone.Factory
{
    [RegisterBaselineMessage(MessageOp.FCYT, 0x06)]
    public class FactoryCrateObjectMessage6 : BaselineMessage
    {
        public FactoryCrateObjectMessage6()
        {
        }

        public FactoryCrateObjectMessage6(byte[] Data, int Size, bool parseFromData = false) : base(Data, Size, parseFromData)
        {
        }

        public FactoryCrateObjectMessage6(Message message, bool parseFromData = false) : base(message, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
            {
                return false;
            }



            return true;
        }
    }
}
