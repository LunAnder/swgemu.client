using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SWG.Client.Utils.Attribute;



namespace SWG.Client.Network.Messages.Zone.Object
{
    [ObjectControllerMessage(0x166)]
    public class FlourishMessage : ObjectControllerMessage
    {
        public int FlourishId { get; set; }

        public FlourishMessage()
        {
        }
        
        public FlourishMessage(Message message, bool parseFromData = false) 
            : base(message, parseFromData)
        {
        }


        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
            {
                return false;
            }

            FlourishId = ReadInt32();
            return true;
        }
    }
}
