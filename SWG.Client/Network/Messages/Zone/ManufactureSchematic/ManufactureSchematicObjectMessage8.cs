using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Utils;

namespace SWG.Client.Network.Messages.Zone.ManufactureSchematic
{
    [RegisterBaselineMessage(MessageOp.MSCO, 0x08)]
    public class ManufactureSchematicObjectMessage8 : BaselineMessage
    {
        public ManufactureSchematicObjectMessage8()
        {
        }

        public ManufactureSchematicObjectMessage8(byte[] Data, int Size, bool parseFromData = false) : base(Data, Size, parseFromData)
        {
        }

        public ManufactureSchematicObjectMessage8(Message message, bool parseFromData = false) : base(message, parseFromData)
        {
        }
    }
}
