using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Utils;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone.ManufactureSchematic
{
    [RegisterBaselineMessage(MessageOp.MSCO, 0x07)]
    public class ManufactureSchematicObjectMessage7 : BaselineMessage
    {
        public ManufactureSchematicObjectMessage7()
        {
        }

        public ManufactureSchematicObjectMessage7(byte[] Data, int Size, bool parseFromData = false) 
            : base(Data, Size, parseFromData)
        {
        }

        public ManufactureSchematicObjectMessage7(Message message, bool parseFromData = false) 
            : base(message, parseFromData)
        {
        }
    }
}
