using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils.Attribute;



namespace SWG.Client.Network.Messages.Zone.Object
{
    [ObjectControllerMessage(0x71)]
    public class DataTransformMessage : ObjectControllerMessage
    {

        public int MovementCounter { get; set; }
        public float DirectionX { get; set; }
        public float DirectionY { get; set; }
        public float DirectionZ { get; set; }
        public float DirectionW { get; set; }
        public float PositionX { get; set; }
        public float PositionZ { get; set; }
        public float PositionY { get; set; }
        public int Speed { get; set; }

        public DataTransformMessage()
        {
        }

        public DataTransformMessage(Message message, bool parseFromData = false)
            : base(message, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
            {
                return false;
            }

            MovementCounter = ReadInt32();
            DirectionX = ReadFloat();
            DirectionY = ReadFloat();
            DirectionZ = ReadFloat();
            DirectionW = ReadFloat();
            PositionX = ReadFloat();
            PositionZ = ReadFloat();
            PositionY = ReadFloat();

            Speed = ReadInt32();


            return true;
        }
    }
}
