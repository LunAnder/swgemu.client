using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network.Messages.Zone.Intangible
{
    public class IntangibleObjectMessage6 : BaselineMessage
    {
        public int Unknown { get; set; }
        public string STFFileName { get; set; }
        public string STFName { get; set; }
        public string CustomName { get; set; }

        public IntangibleObjectMessage6(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
                return false;

            Unknown = ReadInt32();
            STFFileName = ReadString(Encoding.ASCII);
            ReadInt32(); //spacer
            STFName = ReadString(Encoding.ASCII);
            CustomName = ReadString(Encoding.UTF8);

            return true;
        }
    }
}
