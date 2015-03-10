using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils;

namespace SWG.Client.Network.Messages.Zone.Intangible
{
    [RegisterBaselineMessage(MessageOp.ITNO, 0x03)]
    public class IntangibleObjectMessage3 : BaselineMessage
    {

        public float Complexity { get; set; }
        public string STFFile { get; set; }
        public string STFName { get; set; }
        public string CustomName { get; set; }
        public int Volume { get; set; }
        public int GenericInt { get; set; }

        public IntangibleObjectMessage3() { }

        public IntangibleObjectMessage3(byte[] Data, int Size = 0, bool parseFromData = false)
            : base(Data, Size, parseFromData)
        {
        }

        public IntangibleObjectMessage3(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
                return false;

            Complexity = ReadFloat();

            STFFile = ReadString(Encoding.ASCII);
            ReadInt32(); // space
            STFName = ReadString(Encoding.ASCII);
            CustomName = ReadString(Encoding.UTF8);
            Volume = ReadInt32();
            GenericInt = ReadInt32();

            return true;
        }
    }
}
