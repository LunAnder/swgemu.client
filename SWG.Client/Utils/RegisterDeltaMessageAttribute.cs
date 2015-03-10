using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network;

namespace SWG.Client.Utils
{
    public class RegisterDeltaMessageAttribute : RegisterMessageWithSecondaryAttribute
    {

        public RegisterDeltaMessageAttribute(uint opcode) 
            : base(opcode)
        {
        }

        public RegisterDeltaMessageAttribute(MessageOp opcode)
            : this((uint)opcode)
        {

        }

        public RegisterDeltaMessageAttribute(MessageOp opcode, uint secondary)
            : base(opcode, secondary)
        {
        }

        public RegisterDeltaMessageAttribute()
        {
            
        }
    }
}
