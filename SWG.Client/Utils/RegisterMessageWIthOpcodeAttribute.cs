using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network;

namespace SWG.Client.Utils
{
    public class RegiserMessageWithOpcodeAttribute : Attribute
    {
        public uint OpCode { get; set; }

        public MessageOp OpCodeEnum
        {
            get { return (MessageOp) OpCode; }
            set { OpCode = (uint) value; }
        }

        public RegiserMessageWithOpcodeAttribute()
        {
            
        }
        
        public RegiserMessageWithOpcodeAttribute(uint opcode)
        {
            OpCode = opcode;
        }

        public RegiserMessageWithOpcodeAttribute(MessageOp opcode)
            : this((uint) opcode)
        {

        }
    }
}
