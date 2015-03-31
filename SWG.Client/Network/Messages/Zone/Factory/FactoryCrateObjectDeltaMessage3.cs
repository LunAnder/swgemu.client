using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone.Factory
{
    [RegisterDeltaMessage(MessageOp.FCYT, 0x03)]
    public class FactoryCrateObjectDeltaMessage3 : DeltaMessage
    {
        public int Quantity { get; set; }

        public FactoryCrateObjectDeltaMessage3()
        {
        }

        public FactoryCrateObjectDeltaMessage3(Message message, bool parseFromData = false) 
            : base(message, parseFromData)
        {
        }

        public FactoryCrateObjectDeltaMessage3(byte[] Data, int Size, bool parseFromData = false) 
            : base(Data, Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
            {
                return false;
            }

            for (var i = 0; i < UpdateCount; i++)
            {
                var updateId = ReadInt16();
                switch (updateId)
                {
                    case 0x07:
                        Quantity = ReadInt32();
                        break;
                }
            }

            return true;
        }
    }
}
