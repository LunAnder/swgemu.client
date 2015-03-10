using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network;

namespace SWG.Client.Utils
{
    public class RegisterMessageAttribute : RegiserMessageWithOpcodeAttribute
    {
        public RegisterMessageAttribute()
        {
        }

        public RegisterMessageAttribute(uint opcode) : base(opcode)
        {
        }

        public RegisterMessageAttribute(MessageOp opcode)
            : base(opcode)
        {

        }
    }
}
