using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Utils;

namespace SWG.Client.Network.Messages.Zone.Cell
{
    [RegisterBaselineMessage(MessageOp.SCLT, 0x03)]
    public class CellObjectMessage3 : BaselineMessage
    {

        public int Unknown1 { get; set; }
        public string STFFile { get; set; }
        public string STFName { get; set; }
        public string CustomName { get; set; }
        public int Unknown2 { get; set; }
        public int CellNumber { get; set; }

        public CellObjectMessage3() { }

        public CellObjectMessage3(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
                return false;

            Unknown1 = ReadInt32();
            STFFile = ReadString(Encoding.ASCII);
            ReadInt32(); // spacer
            STFName = ReadString(Encoding.ASCII);
            CustomName = ReadString(Encoding.UTF8);
            Unknown2 = ReadInt32();
            CellNumber = ReadInt32();

            return true;
        }
    }
}
