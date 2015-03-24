using System;
using System.Collections.Concurrent;
using System.Linq;
using LogAbstraction;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.MessageFactories
{
    [FallbackMessageFactory]
    public class DefaultMessageFactory : BaseMessageFactory<OpcodeKey>
    {
        private static readonly ILogger _logger = LogAbstraction.LogManagerFacad.GetCurrentClassLogger();

        protected override ILogger Logger
        {
            get { return _logger; }
        }

        public bool RegisterMessageObject(MessageOp opcode, Func<Message, Message> createFunc)
        {
            return RegisterMessageObject((uint)opcode, createFunc);
        }

        public bool RegisterMessageObject<T>(MessageOp opcode)
            where T : Message, new()
        {
            return RegisterMessageObject<T>((uint)opcode);
        }

        public virtual bool RegisterMessageObject(uint opcode, Func<Message, Message> createFunc)
        {
            return RegisteredObjects.TryAdd(opcode, createFunc);
        }

        public virtual bool RegisterMessageObject<T>(uint opcode)
            where T : Message, new()
        {
            return RegisterMessageObject(opcode, (msg) => Message.Create<T>(msg));
        }

        protected override OpcodeKey GetKey(uint messageOp, Message baseMessage)
        {
            return messageOp;
        }

        public override bool RegisterMessageObjectFromAttribute<T>()
        {
            var registered = false;

            var attrs =
                typeof (T).GetCustomAttributes(typeof (RegisterMessageAttribute), false)
                    .Cast<RegisterMessageAttribute>();
            foreach (var attr in attrs)
            {
                RegisterMessageObject<T>(attr.OpCode);
                registered = true;
            }

            return registered;
        }
    }
}
