using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network
{
    public enum ObjectControllerFlags : int
    {
        NONE = 0x00000000,
        SEND = 0x00000001,
        RELIABLE = 0x00000002,
        SOURCE_REMOTE_SERVER = 0x00000004,
        DEST_AUTH_CLIENT = 0x00000008,
        DEST_PROXY_CLIENT = 0x00000010,
        DEST_AUTH_SERVER = 0x00000020,
        DEST_PROXY_SERVER = 0x00000040,
        SOURCE_REMOTE_CLIENT = 0x00000100,

        ALL =
            NONE | SEND | RELIABLE | SOURCE_REMOTE_SERVER | DEST_AUTH_CLIENT | DEST_PROXY_CLIENT | DEST_AUTH_SERVER |
            DEST_PROXY_SERVER | SOURCE_REMOTE_CLIENT,
    }
}
