using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network
{
    public class SessionLoginCompleteEventArgs : EventArgs
    {
        public byte[] SessionKey { get; private set; }
        public AvailableServer[] AvailableServers { get; private set; }
        
        public SessionLoginCompleteEventArgs(byte[] sessionKey, AvailableServer[] availableServers)
        {
            SessionKey = sessionKey;
            AvailableServers = availableServers;
        }
    }
}
