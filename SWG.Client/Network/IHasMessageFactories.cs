using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network
{
    public interface IHasMessageFactories
    {
        ConcurrentDictionary<uint, IMessageParseFactory> MessageFactories { get; set; }
    }
}
