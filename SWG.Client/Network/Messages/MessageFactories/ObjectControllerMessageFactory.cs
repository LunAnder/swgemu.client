using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogAbstraction;
using SWG.Client.Network.Messages.MessageFactories.Keys;
using SWG.Client.Network.Messages.Zone.Object;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.MessageFactories
{
    [MessageFactory(MessageOp.ObjControllerMessage)]
    public class ObjectControllerMessageFactory : BaseMessageFactory<ObjectControllerKey>
    {
        private static readonly ILogger _logger = LogManagerFacad.GetCurrentClassLogger();

        protected override ILogger Logger
        {
            get { return _logger; }
        }

        protected override ObjectControllerKey GetKey(uint messageOp, Message baseMessage)
        {
            using (baseMessage.TemporaryRead())
            {
                baseMessage.ReadIndex = 10;
                return (uint)baseMessage.ReadInt32();
            }
        }

        public virtual bool RegisterMessageObject<T>(uint opcode)
            where T : Message, new()
        {
            return RegisterMessageObject(opcode, (msg) => Message.Create<T>(msg));
        }

        public virtual bool RegisterMessageObject(uint opcode, Func<Message, Message> createFunc)
        {
            return RegisteredObjects.TryAdd(opcode, createFunc);
        }

        public override bool RegisterMessageObjectFromAttribute<T>()
        {
            var registered = false;

            var attrs =
                typeof(T).GetCustomAttributes(typeof(ObjectControllerMessageAttribute), false)
                    .Cast<ObjectControllerMessageAttribute>();
            foreach (var attr in attrs)
            {
                RegisterMessageObject<T>(attr.OpCode);
                registered = true;
            }

            return registered;
        }
    }
}
