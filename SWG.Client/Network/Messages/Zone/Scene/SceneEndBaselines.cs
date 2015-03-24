using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone.Scene
{
    [RegisterMessage(MessageOp.SceneEndBaselines)]
    public class SceneEndBaselines : Message
    {

        public long ObjectId { get; set; }

         public SceneEndBaselines() { }

         public SceneEndBaselines(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            ReadIndex = 6;

            ObjectId = ReadInt64();
            return true;
        }
    }
}
