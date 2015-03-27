using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SWG.Client.Utils.Attribute;



namespace SWG.Client.Network.Messages.Zone.Object
{
    [ObjectControllerMessage(0x12E)]
    public class EmoteMessage : ObjectControllerMessage
    {
        public long SenderObjectId { get; set; }
        public long TargetObjectId { get; set; }
        public int EmoteId { get; set; }
        public byte ShowText { get; set; }

        public EmoteMessage()
        {
        }

        public EmoteMessage(Message message, bool parseFromData = false) : base(message, parseFromData)
        {
        }


        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
            {
                return false;
            }

            SenderObjectId = ReadInt64();
            TargetObjectId = ReadInt64();
            EmoteId = ReadInt32();
            ShowText = ReadByte();

            return true;
        }
    }
}
