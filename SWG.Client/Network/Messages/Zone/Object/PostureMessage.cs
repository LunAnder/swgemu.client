using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone.Object
{
    [ObjectControllerMessage(0x131)]
    class PostureMessage : ObjectControllerMessage
    {
        public byte Posture { get; set; }
        public byte Active { get; set; }
        public PostureMessage()
        {
        }

        public PostureMessage(Message message, bool parseFromData = false)
            : base(message, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
            {
                return false;
            }

            Posture = ReadByte();
            Active = ReadByte();
            return true;
        }
    }
}
