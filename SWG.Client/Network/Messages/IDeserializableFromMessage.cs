using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network.Messages
{
    public interface IDeserializableFromMessage<T>
    {
        T Deserialize(IDataContainerRead DataContainer);
    }
}
