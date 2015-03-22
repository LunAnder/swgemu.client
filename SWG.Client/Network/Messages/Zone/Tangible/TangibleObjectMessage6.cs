using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Utils;

namespace SWG.Client.Network.Messages.Zone.Tangible
{
    [RegisterBaselineMessage(MessageOp.WeaponObjectMessage, 0x06)]
    [RegisterBaselineMessage(MessageOp.TANO, 0x06)]
    public class TangibleObjectMessage6 : BaselineMessage
    {
        public int ServerId { get; set; }
        public long[] DefenderObjectIds { get; set; }

        public TangibleObjectMessage6() { }

        public TangibleObjectMessage6(byte[] Data, int Size, bool parseFromData = false)
                : base(Data, Size, parseFromData)
        {
            
        }

        public TangibleObjectMessage6(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
                return false;
            ServerId = ReadInt32();

            DefenderObjectIds = ReadList(ReadInt64);
                
            return true;
        }
    }
}
