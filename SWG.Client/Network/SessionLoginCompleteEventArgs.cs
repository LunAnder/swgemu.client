using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network
{
    public class SessionLoginCompleteEventArgs : EventArgs
    {
        public ulong UserId { get; private set; }
        public byte[] SessionKey { get; private set; }
        public AvailableServer[] AvailableServers { get; private set; }
        
        public SessionLoginCompleteEventArgs(ulong userId, byte[] sessionKey, AvailableServer[] availableServers)
        {
            UserId = UserId;
            SessionKey = sessionKey;
            AvailableServers = availableServers;
        }
    }
}
