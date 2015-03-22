using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network
{
    public interface IMessageParseFactory
    {
        bool TryParse(uint messageOp, Message baseMessage, out Message parsedMessage);
        bool RegisterMessageObjectFromAttribute<T>()
            where T : Message, new();

        bool RegisterMessageObjectFromAttribute(Type toRegister);
    }
}
