using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using SWG.Client.Network.Objects;

namespace SWG.Client.Network
{
    public class AvailableServer : ServerDetails
    {
        public string Name { get; set; }
        public int Timezone { get; set; }

        public List<Character> Characters { get; set; }

        public AvailableServer()
        {
            
        }

        public AvailableServer(ServerDetails details, ServerName name, IEnumerable<Character> characters)
        {
            Distance = details.Distance;
            MaxCapacity = details.MaxCapacity;
            NotRecommended = details.NotRecommended;
            MaxCharsPerServer = details.MaxCharsPerServer;
            PingPort = details.PingPort;
            Population = details.Population;
            ServerID = details.ServerID;
            ServerIP = details.ServerIP;
            ServerPort = details.ServerPort;
            Status = details.Status;

            Name = name.ServerDisplayName;
            Timezone = name.Timezone;

            Characters = characters != null ? characters.ToList() : null;
        }
 
    }
}
