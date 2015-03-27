using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils.Attribute;


namespace SWG.Client.Network.Messages.Zone.Object
{
    [ObjectControllerMessage(0xF2)]
    public class AnimationMessage : ObjectControllerMessage
    {
        public string Animation { get; set; }

        public AnimationMessage()
        {
        }

        public AnimationMessage(Message message, bool parseFromData = false)
            : base(message, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
            {
                return false;
            }

            Animation = ReadString(Encoding.ASCII);
            return true;
        }
    }
}
