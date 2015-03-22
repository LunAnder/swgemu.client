using System;
using SWG.Client.Network;

namespace SWG.Client.Utils.Attribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RegiserMessageWithOpcodeAttribute : System.Attribute
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
