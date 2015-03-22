using SWG.Client.Network;

namespace SWG.Client.Utils.Attribute
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
