using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SWG.Client.Object.Scene.Variables;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone.Object
{
    [ObjectControllerMessage(0xF4)]
    public class SpatialChatMessage : ObjectControllerMessage
    {
        public long SenderId { get; set; }
        public long TargetId { get; set; }
        public string Message { get; set; }
        public int Unknown1 { get; set; }
        public short MoodPrimary { get; set; }
        public short MoodSecondary { get; set; }
        public byte Unknown2 { get; set; }
        public byte LanguageId { get; set; }

        public StringId ChatParamater { get; set; }


        public SpatialChatMessage()
        {
        }
        
        public SpatialChatMessage(Message message, bool parseFromData = false)
                : base(message, parseFromData)
        {
        }


        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
            {
                return false;
            }

            SenderId = ReadInt64();
            TargetId = ReadInt64();
            Message = ReadString(Encoding.UTF8);
            Unknown1 = ReadInt32();
            MoodPrimary = ReadInt16();
            MoodSecondary = ReadInt16();
            Unknown2 = ReadByte();
            LanguageId = ReadByte();

            var hasStringId = PeekInt32();
            if (hasStringId != 0)
            {
                ChatParamater = new StringId
                {
                    File = ReadString(Encoding.ASCII),
                    Id = ReadString(Encoding.ASCII),

                };
            }

            return true;

        }
    }
}
