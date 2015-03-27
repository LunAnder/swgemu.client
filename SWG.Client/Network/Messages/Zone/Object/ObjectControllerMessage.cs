using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network.Messages.Zone.Object
{
    public class ObjectControllerMessage : Message
    {
        public long ObjectId { get; set; }
        public int ControllerType { get; set; }
        public int Flags { get; set; }
        public int Ticks { get; set; }

        public ObjectControllerFlags FlagsEnum
        {
            get { return (ObjectControllerFlags) Flags; }
            set { Flags = (int) value; }
        }


        public ObjectControllerMessage()
        {
        }

        public ObjectControllerMessage(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            ReadIndex = 6;

            Flags = ReadInt32();
            ControllerType = ReadInt32();
            ObjectId = ReadInt64();
            Ticks = ReadInt32();

            return true;
        }


        protected List<T> ReadList<T>(Func<int> readSizeFunc)
            where T : IDeserializableFromMessage<T>, new()
        {
            var size = readSizeFunc();
            var toReturn = new List<T>(size);
            for (int i = 0; i < size; i++)
            {
                toReturn[i] = (new T()).Deserialize(this);
            }
            return toReturn;
        }

    }
}
