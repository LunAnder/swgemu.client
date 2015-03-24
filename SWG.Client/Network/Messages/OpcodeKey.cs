using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network.Messages
{
    public class OpcodeKey
    {
        public uint Opcode { get; set; }

        public MessageOp OpcodeEnum
        {
            get { return (MessageOp) Opcode; }
            set { Opcode = (uint) value; }
        }

        public OpcodeKey(uint opcode)
        {
            Opcode = opcode;
        }

        public OpcodeKey(MessageOp opcode)
        {
            OpcodeEnum = opcode;
        }


        public override string ToString()
        {
            return string.Format("{0} {1:X}", OpcodeEnum, Opcode);
        }

        public override int GetHashCode()
        {
            return Opcode.GetHashCode();
        }

        public static implicit operator uint(OpcodeKey opcodeKey)
        {
            return opcodeKey.Opcode;
        }

        public static implicit operator OpcodeKey(uint opcode)
        {
            return new OpcodeKey(opcode);
        }

        public static implicit operator MessageOp(OpcodeKey opcodeKey)
        {
            return opcodeKey.OpcodeEnum;
        }

        public static implicit operator OpcodeKey(MessageOp opcode)
        {
            return new OpcodeKey(opcode);
        }
    }
}
