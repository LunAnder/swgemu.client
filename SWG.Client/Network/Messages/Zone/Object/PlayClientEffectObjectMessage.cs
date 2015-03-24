using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone.Object
{
    [RegisterMessage(MessageOp.PlayClientEffectObjectMessage)]
    public class PlayClientEffectObjectMessage : Message
    {
        public long ObjectId { get; set; }
        public string FileName { get; set; }
        public string StringId { get; set; }

        public PlayClientEffectObjectMessage()
        {
        }

        public PlayClientEffectObjectMessage(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            ReadIndex = 6;

            FileName = ReadString(Encoding.ASCII);
            StringId = ReadString(Encoding.ASCII);
            ObjectId = ReadInt64();
            return true;
        }
    }
}
