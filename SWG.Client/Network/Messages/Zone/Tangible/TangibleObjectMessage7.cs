using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network.Messages.Zone.Tangible
{
    public class TangibleObjectMessage7 : BaselineMessage
    {
        public long ObjectId1 { get; set; }
        public long ObjectId2 { get; set; }


        public TangibleObjectMessage7(byte[] Data, int Size, bool parseFromData = false)
                : base(Data, Size, parseFromData)
        {
            
        }

        public TangibleObjectMessage7(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
                return false;

            ObjectId1 = ReadInt64();
            ObjectId2 = ReadInt64();

            return true;
        }
    }
}
