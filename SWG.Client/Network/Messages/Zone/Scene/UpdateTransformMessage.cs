using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone.Scene
{

    [RegisterMessage(MessageOp.UpdateTransformMessage)]
    public class UpdateTransformMessage : Message
    {
        public long ObjectId { get; set; }
        public short X { get; set; }
        public short Z { get; set; }
        public short Y { get; set; }
        public int MovementCounter { get; set; }
        public byte Direction { get; set; }


        public UpdateTransformMessage() { }

        public UpdateTransformMessage(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            ReadIndex = 6;

            ObjectId = ReadInt64();

            X = ReadInt16();
            Z = ReadInt16();
            Y = ReadInt16();
            MovementCounter = ReadInt32();

            ReadByte();
            Direction = ReadByte();

            return true;
        }
    }
}
