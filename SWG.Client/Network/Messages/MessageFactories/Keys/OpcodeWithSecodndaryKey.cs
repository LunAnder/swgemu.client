using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils;

namespace SWG.Client.Network.Messages.MessageFactories
{
    public class OpcodeWithSecodndaryKey : Tuple<uint,uint>
    {
        public OpcodeWithSecodndaryKey(uint opcode, uint secondary) 
            : base(opcode, secondary)
        {
            
        }

        public override string ToString()
        {
            return string.Format("({0}: {1:X},{2:X})",(MessageOp)Item1, Item1, Item2);
        }
    }
}
