using SWG.Client.Network;

namespace SWG.Client.Utils.Attribute
{
    public class MessageFactoryAttribute : RegiserMessageWithOpcodeAttribute
    {
        public MessageFactoryAttribute()
        {

        }

        public MessageFactoryAttribute(uint opcode)
        {
            OpCode = opcode;
        }

        public MessageFactoryAttribute(MessageOp opcode)
            : this((uint) opcode)
        {

        }
    }
}
