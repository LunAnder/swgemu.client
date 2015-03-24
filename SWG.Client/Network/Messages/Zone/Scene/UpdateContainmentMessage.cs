using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone.Scene
{
    [RegisterMessage(MessageOp.UpdateContainmentMessage)]
    public class UpdateContainmentMessage : Message
    {
        public long ContainerId { get; set; }
        public long ObjectId { get; set; }
        public int ContainmentType { get; set; }

         public UpdateContainmentMessage() { }

         public UpdateContainmentMessage(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            ReadIndex = 6;

            ObjectId = ReadInt64();
            ContainerId = ReadInt64();
            ContainmentType = ReadInt32();

            return true;
        }
    }
}
