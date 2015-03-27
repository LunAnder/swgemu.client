using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SWG.Client.Utils.Attribute;



namespace SWG.Client.Network.Messages.Zone.Object
{
    [ObjectControllerMessage(0x1BD)]
    public class ShowFlyTextMessage : ObjectControllerMessage
    {
        public long ObjectId { get; set; }
        public string File { get; set; }
        public string Message { get; set; }
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
        public byte DisplayFlag { get; set; }


        public ShowFlyTextMessage()
        {
        }

        public ShowFlyTextMessage(Message message, bool parseFromData = false) 
            : base(message, parseFromData)
        {
        }


        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
            {
                return false;
            }

            ObjectId = ReadInt64();
            File = ReadString(Encoding.ASCII);
            ReadInt32();
            Message = ReadString(Encoding.ASCII);
            ReadInt32();
            Red = ReadByte();
            Green = ReadByte();
            Blue = ReadByte();
            DisplayFlag = ReadByte();

            return true;
        }
    }
}
