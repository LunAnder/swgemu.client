using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network
{
    public enum SessionStatus
    {
        Initialize = 0,
        Connecting,
        Connected,
        Disconnecting,
        Disconnected,
        Destroy,
        Timeout,
        Error
    };
}
