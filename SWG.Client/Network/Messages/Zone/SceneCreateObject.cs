using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone
{
    [RegisterMessage(MessageOp.SceneCreateObjectByCrc)]
    public class SceneCreateObject : Message
    {

        public long ObjectID { get; set; }
        public float QuaternionX { get; set; }
        public float QuaternionY { get; set; }
        public float QuaternionZ { get; set; }
        public float QuaternionW { get; set; }
        public float XCoorinate { get; set; }
        public float YCoorinate { get; set; }
        public float ZCoorinate { get; set; }
        public int ObjectCRC { get; set; }
        public byte ByteFlag { get; set; }

        public SceneCreateObject() { }
        

        public SceneCreateObject(byte[] Data, int Size = 0, bool parseFromData = false)
             : base(Data, Size, parseFromData)
        {
        }

        public SceneCreateObject(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            ReadIndex = 6;

            ObjectID = ReadInt64();
            QuaternionX = ReadFloat();
            QuaternionY = ReadFloat();
            QuaternionZ = ReadFloat();
            QuaternionW = ReadFloat();
            XCoorinate = ReadFloat();
            ZCoorinate = ReadFloat();
            YCoorinate = ReadFloat();
            ObjectCRC = ReadInt32();
            ByteFlag = ReadByte();
            
            return true;
        }
    }
}
