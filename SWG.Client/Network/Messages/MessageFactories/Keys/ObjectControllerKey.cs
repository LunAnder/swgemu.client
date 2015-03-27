using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network.Messages.MessageFactories.Keys
{
    public class ObjectControllerKey
    {
        public uint ControllerType { get; set; }

        public ObjectControllerKey(uint controllerType)
        {
            ControllerType = controllerType;
        }


        public override string ToString()
        {
            return string.Format("{0:X}", ControllerType);
        }

        public override int GetHashCode()
        {
            return ControllerType.GetHashCode();
        }

        public static implicit operator uint(ObjectControllerKey opcodeKey)
        {
            return opcodeKey.ControllerType;
        }

        public static implicit operator ObjectControllerKey(uint opcode)
        {
            return new ObjectControllerKey(opcode);
        }
    }
}
