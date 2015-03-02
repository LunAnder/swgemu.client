using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network
{
    public enum SessionOp : uint
    {
        First = 0x0000u,
        SessionRequest = 0x0100u,
        SessionResponse = 0x0200u,
        MultiPacket = 0x0300u,
        Unknown1 = 0x0400u,
        Disconnect = 0x0500u,
        Ping = 0x0600u,
        NetStatRequest = 0x0700u,
        NetStatResponse = 0x0800u,
        DataChannel1 = 0x0900u,
        DataChannel2 = 0x0a00u,
        DataChannel3 = 0x0b00u,
        DataChannel4 = 0x0c00u,
        DataFrag1 = 0x0d00u,
        DataFrag2 = 0x0e00u,
        DataFrag3 = 0x0f00u,
        DataFrag4 = 0x1000u,
        DataOrder1 = 0x1100u,
        DataOrder2 = 0x1200u,
        DataOrder3 = 0x1300u,
        DataOrder4 = 0x1400u,
        DataAck1 = 0x1500u,
        DataAck2 = 0x1600u,
        DataAck3 = 0x1700u,
        DataAck4 = 0x1800u,
        FatalError = 0x1900u,
        FatalErrorResponse = 0x1a00u,
        Reset = 0x1d00u,
        CriticalError = 0x1e00u
    };
}
