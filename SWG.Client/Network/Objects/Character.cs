using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network.Objects
{
    public class Character 
    {
        public string Name { get; set; }
        public int RaceGenderCRC { get; set; }
        public long CharacterID { get; set; }
        public int ServerID { get; set; }
        public int Status { get; set; }
    }
}
