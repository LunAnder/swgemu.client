using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using SWG.Client.Network;

namespace SWG.Client.Utils.Attribute
{
    public class HandledByFactoryAttribute : RegiserMessageWithOpcodeAttribute
    {

        public uint FactoryHandlerOpCode { get; set; }

        public MessageOp FactoryHandlerOpCodeEnum
        {
            get { return (MessageOp) FactoryHandlerOpCode; }
            set { FactoryHandlerOpCode = (uint) value; }
        }

        public HandledByFactoryAttribute()
        {
            
        }
        
        public HandledByFactoryAttribute(uint opcode)
        {
            OpCode = opcode;
        }

        public HandledByFactoryAttribute(MessageOp opcode)
            : this((uint) opcode)
        {

        }
    }
}
