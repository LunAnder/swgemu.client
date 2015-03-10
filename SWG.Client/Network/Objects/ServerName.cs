using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network.Objects
{
    public class ServerName
    {
        public int ServerID { get; set; }
        public string ServerDisplayName { get; set; }
        public int Timezone { get; set; }
    }
}
