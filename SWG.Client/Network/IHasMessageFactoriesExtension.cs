using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network
{
    public static class IHasMessageFactoriesExtension
    {
        public static void ResolveMessageFactories(this IHasMessageFactories target, IEnumerable<Type> toResolveFrom = null)
        {
            if (toResolveFrom == null)
            {
                toResolveFrom = new AssemblyTypesEnumerator();
            }

            foreach (
                var type in toResolveFrom
                    .Where(cur => cur.GetCustomAttributes(typeof (MessageFactoryAttribute), false).Length >= 1))
            {
                var attr =
                    (MessageFactoryAttribute)
                        type.GetCustomAttributes(typeof (MessageFactoryAttribute), false).First();

                target.MessageFactories.TryAdd(attr.OpCode,
                    (IMessageParseFactory) type.GetConstructor(new Type[] {}).Invoke(new object[] {}));
            }
        }

        public static void RegisterMessagesInFacories(this IHasMessageFactories target,
            IEnumerable<Type> toResolveFrom = null)
        {
            if (toResolveFrom == null)
            {
                toResolveFrom = new AssemblyTypesEnumerator();
            }

            foreach (var type in toResolveFrom)
            {
                var attrs = type.GetCustomAttributes(typeof (HandledByFactoryAttribute), true).Cast<HandledByFactoryAttribute>();
                foreach (var attr in attrs)
                {
                    IMessageParseFactory factory;
                    if (target.MessageFactories.TryGetValue(attr.FactoryHandlerOpCode, out factory))
                    {
                        factory.RegisterMessageObjectFromAttribute(type);
                    }
                }
            }
        } 
    }
}
