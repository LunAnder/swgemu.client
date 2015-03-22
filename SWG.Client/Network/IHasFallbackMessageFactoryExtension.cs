using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network
{
    public static class IHasFallbackMessageFactoryExtension
    {
        public static void ResolveFallbackMessageFactory(this IHasFallbackMessageFactory target, IEnumerable<Type> toResolveFrom = null)
        {
            if (toResolveFrom == null)
            {
                toResolveFrom = new AssemblyTypesEnumerator();
            }

            foreach (var type in toResolveFrom
                .Where(cur => cur.GetCustomAttributes(typeof (FallbackMessageFactoryAttribute), false).Length >= 1))
            {
                target.FallbackFactory =
                    (IMessageParseFactory) type.GetConstructor(new Type[] {}).Invoke(new object[] {});
            }
        }

        public static void RegisterMessagesInFallbackFactory(this IHasFallbackMessageFactory target,
            IEnumerable<Type> toResolveFrom = null)
        {
            if (toResolveFrom == null)
            {
                toResolveFrom = new AssemblyTypesEnumerator();
            }

            foreach (var type in toResolveFrom)
            {
                if (type.GetCustomAttributes(typeof (RegisterMessageAttribute), false).Length > 0)
                {
                    target.FallbackFactory.RegisterMessageObjectFromAttribute(type);
                }
            }
        }
    }
}
