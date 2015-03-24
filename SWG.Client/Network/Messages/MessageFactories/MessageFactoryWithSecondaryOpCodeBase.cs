using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.MessageFactories
{
    public abstract class MessageFactoryWithSecondaryOpCodeBase : BaseMessageFactory<OpcodeWithSecodndaryKey>
    {
        protected override OpcodeWithSecodndaryKey GetKey(uint messageOp, Message baseMessage)
        {
            baseMessage.ReadIndex = 14;
            var opcode = baseMessage.ReadUInt32();
            var secondary = baseMessage.ReadByte();

            return new OpcodeWithSecodndaryKey(opcode, secondary);
        }

        public override bool RegisterMessageObjectFromAttribute<T>()
        {
            var registered = false;

            foreach (var registerAttr in typeof(T).GetCustomAttributes(typeof(RegisterMessageWithSecondaryAttribute), false).Cast<RegisterMessageWithSecondaryAttribute>())
            {
                registered = true;
                RegisterMessageObject<T>(registerAttr.OpCode, registerAttr.Secondary.Value);
            }

            return registered;
        }

        public bool RegisterMessageObject(MessageOp opcode, uint secondary, Func<Message, Message> createFunc)
        {
            return RegisterMessageObject((uint)opcode, secondary, createFunc);
        }

        public bool RegisterMessageObject<T>(MessageOp opcode, uint secondary)
            where T : Message, new()
        {
            return RegisterMessageObject<T>((uint)opcode, secondary);
        }

        public virtual bool RegisterMessageObject(uint opcode, uint secondary, Func<Message, Message> createFunc)
        {
            return RegisteredObjects.TryAdd(new OpcodeWithSecodndaryKey(opcode, secondary), createFunc);
        }

        public virtual bool RegisterMessageObject<T>(uint opcode, uint secondary)
            where T : Message, new()
        {
            return RegisterMessageObject(opcode, secondary, (msg) => Message.Create<T>(msg));
        }

    }
}
