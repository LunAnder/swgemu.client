using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogAbstraction;

namespace SWG.Client.Network.Messages.MessageFactories
{
    public abstract class BaseMessageFactory<TKey> : IMessageParseFactory
    {
        protected abstract ILogger Logger { get; }

        public readonly ConcurrentDictionary<TKey, Func<Message, Message>> RegisteredObjects =
            new ConcurrentDictionary<TKey, Func<Message, Message>>();

        public bool TryParse(uint messageOp, Message baseMessage, out Message parsedMessage)
        {
            Func<Message, Message> msgParser = null;
            var key = GetKey(messageOp, baseMessage);
            if (!RegisteredObjects.TryGetValue(key, out msgParser))
            {
                parsedMessage = baseMessage;
                Logger.Warn("Unable to locate parse function for key {0}", key);
                return false;
            }

            parsedMessage = msgParser(baseMessage);
            return true;
        }

        protected abstract TKey GetKey(uint messageOp, Message baseMessage);

        public abstract bool RegisterMessageObjectFromAttribute<T>()
            where T : Message, new();

        public virtual bool RegisterMessageObjectFromAttribute(Type toRegister)
        {
            return (bool)GetType()
                .GetMethod("RegisterMessageObjectFromAttribute", new Type[] { })
                .MakeGenericMethod(new[] { toRegister })
                .Invoke(this, new object[] { });
        }
    }
}
