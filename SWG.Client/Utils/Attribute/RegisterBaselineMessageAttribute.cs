using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Utils
{

    public class RegisterBaselineMessageAttribute : RegisterMessageWithSecondaryAttribute
    {
        public RegisterBaselineMessageAttribute(uint opcode) 
            : base(opcode)
        {
            FactoryHandlerOpCodeEnum = MessageOp.BaselinesMessage;
        }

        public RegisterBaselineMessageAttribute(MessageOp opcode)
            : this((uint)opcode)
        {

        }

        public RegisterBaselineMessageAttribute(MessageOp opcode, uint secondary)
            : base(opcode, secondary)
        {
            FactoryHandlerOpCodeEnum = MessageOp.BaselinesMessage;
        }

        public RegisterBaselineMessageAttribute()
        {
            FactoryHandlerOpCodeEnum = MessageOp.BaselinesMessage;
        }
    }
}
