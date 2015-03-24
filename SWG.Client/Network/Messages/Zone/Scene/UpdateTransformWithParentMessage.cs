using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone.Scene
{
    [RegisterMessage(MessageOp.UpdateTransformMessageWithParent)]
    public class UpdateTransformWithParentMessage : Message
    {

        public long ParentObjectId { get; set; }
        public long ObjectId { get; set; }
        public short X { get; set; }
        public short Z { get; set; }
        public short Y { get; set; }
        public int MovementCounter { get; set; }
        public byte Direction { get; set; }


        public UpdateTransformWithParentMessage() { }

        public UpdateTransformWithParentMessage(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }


        public override bool ParseFromData()
        {
            ReadIndex = 6;

            ParentObjectId = ReadInt64();

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
