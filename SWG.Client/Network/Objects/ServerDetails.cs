using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network.Objects
{
    public class ServerDetails
    {
        public int ServerID { get; set; }
        public string ServerIP { get; set; }
        public UInt16 ServerPort { get; set; }
        public UInt16 PingPort { get; set; }
        public int Population { get; set; }
        public int MaxCapacity { get; set; }
        public int MaxCharsPerServer { get; set; }
        public int Distance { get; set; }
        public int Status { get; set; }
        public bool NotRecommended { get; set; }
    }
}
