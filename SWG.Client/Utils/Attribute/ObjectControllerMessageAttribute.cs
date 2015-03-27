using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network;

namespace SWG.Client.Utils.Attribute
{
    public class ObjectControllerMessageAttribute : HandledByFactoryAttribute
    {
        public ObjectControllerMessageAttribute(uint opcode)
            : base(opcode)
        {
            FactoryHandlerOpCodeEnum = MessageOp.ObjControllerMessage;
        }

        public ObjectControllerMessageAttribute(MessageOp opcode)
            : this((uint) opcode)
        {

        }
    }
}
