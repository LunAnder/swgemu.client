using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network;

namespace SWG.Client.Utils
{
    public class RegisterMessageWithSecondaryAttribute : RegiserMessageWithOpcodeAttribute
    {
        public uint? Secondary { get; set; }

        public RegisterMessageWithSecondaryAttribute(uint opcode)
            : base(opcode)
        {
        }

        public RegisterMessageWithSecondaryAttribute(uint opcode, uint secondary)
            : this(opcode)
        {
            Secondary = secondary;
        }

        public RegisterMessageWithSecondaryAttribute(MessageOp opcode, uint secondary)
            : this((uint)opcode, secondary)
        {
        }

        public RegisterMessageWithSecondaryAttribute()
        {
            
        }
    }
}
