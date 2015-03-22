using System;

namespace SWG.Client.Network.Messages.Base
{
    public class BaselineMessage : Message
    {

        public long ObjectId { get; set; }
        public int ObjectType { get; set; }
        public byte TypeNumber { get; set; }
        public int DataSize { get; set; }
        public short ObjOpcodeCount { get; set; }

        public BaselineMessage()
        {
            
        }

        public BaselineMessage(byte[] Data, int Size, bool parseFromData = false)
                : base(Data, Size, parseFromData)
        {
            
        }

        public BaselineMessage(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            ReadIndex = 6;
            ObjectId = ReadInt64();
            ObjectType = ReadInt32();
            TypeNumber = ReadByte();
            DataSize = ReadInt32();
            ObjOpcodeCount = ReadInt16();

            return true;
        }

        /// <summary>
        /// reads a list of items using the creat object func for each item in the list to read the raw data for the objec
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="CreateObjectFunc">A function to read the data and create an object. called once for each object in the data</param>
        /// <param name="HasUpdateCounter">True if there is an update counter after the list size, false if there is not</param>
        /// <param name="ReadUpdateType">True to read the update type preceeding each object, false to not</param>
        /// <returns></returns>
        public virtual T[] ReadList<T>(Func<T> CreateObjectFunc, bool HasUpdateCounter = true, bool ReadUpdateType = false)
        {
            var size = ReadInt32();
            var updateCount = -1;
            //update counter, not useful here
            if (HasUpdateCounter)
            {
                updateCount = ReadInt32();
            }

            var arr = new T[size];
            
            for (int i = 0; i < size /*&& ReadIndex <= Data.Length*/; i++)
            {
                if (ReadUpdateType)
                {
                    ReadByte();
                }

                arr[i] = CreateObjectFunc();
            }

            return arr;
        }

        /// <summary>
        /// Reads a list of T objects usining the objects deserialize method to parse the object from the data stream
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="HasUpdateCounter">True if there is an update counter after the list size, false if there is not</param>
        /// <param name="ReadUpdateType">True to read the update type preceeding each object, false to not</param>>
        /// <returns></returns>
        public T[] ReadList<T>(bool HasUpdateCounter = true, bool ReadUpdateType = false)
                where T : IDeserializableFromMessage<T>,new()
        {

            return ReadList<T>(() => ((new T()).Deserialize(this)), HasUpdateCounter, ReadUpdateType);
        }
    }
}
