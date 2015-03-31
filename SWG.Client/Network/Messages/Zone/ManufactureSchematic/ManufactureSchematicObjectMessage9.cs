using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Utils;

namespace SWG.Client.Network.Messages.Zone.ManufactureSchematic
{
    [RegisterBaselineMessage(MessageOp.MSCO, 0x09)]
    public class ManufactureSchematicObjectMessage9 : BaselineMessage
    {
        public ManufactureSchematicObjectMessage9()
        {
        }

        public ManufactureSchematicObjectMessage9(byte[] Data, int Size, bool parseFromData = false)
            : base(Data, Size, parseFromData)
        {
        }

        public ManufactureSchematicObjectMessage9(Message message, bool parseFromData = false)
            : base(message, parseFromData)
        {
        }
    }
}
