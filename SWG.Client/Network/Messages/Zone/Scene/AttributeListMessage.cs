using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone.Scene
{
    [RegisterMessage(MessageOp.AttributeListMessage)]
    public class AttributeListMessage : Message
    {

        public long ObjectId { get; set; }

        public Dictionary<string, string> Attributes { get; set; }

        public AttributeListMessage()
        {
            
        }

        public AttributeListMessage(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }


        public override bool ParseFromData()
        {
            ReadIndex = 6;

            ObjectId = ReadInt64();

            var count = ReadInt32();

            Attributes = new Dictionary<string, string>(count);

            for (int i = 0; i < count; i++)
            {
                Attributes.Add(ReadString(Encoding.ASCII), ReadString(Encoding.Unicode));
            }
            
            return true;

        }

    }
}
