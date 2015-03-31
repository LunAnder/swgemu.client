using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Messages.Base;

namespace SWG.Client.Network.Messages
{
    public class DeltaMessage : Message
    {

        public long ObjectId { get; set; }
        public int ObjectType { get; set; }
        public byte TypeNumber { get; set; }
        public int DataSize { get; set; }
        public short UpdateCount { get; set; }

        public DeltaMessage() { }

        public DeltaMessage(byte[] Data, int Size, bool parseFromData = false)
            : base(Data, Size, parseFromData)
        {

        }

        public DeltaMessage(Message message, bool parseFromData = false)
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
            UpdateCount = ReadInt16();

            return true;
        }


        public virtual ListChange<T>[] ReadListChanges<T>(Func<ListChange<T>> CreateChange)
        {
            //size, nut sure if we need to use this here.
            var size = ReadInt32();
           var udpates = ReadInt32();

           var arr = new ListChange<T>[size];

           for (int i = 0; i < size; i++)
            {
                arr[i] = CreateChange();
            }

            return arr;
        }

        public virtual ListChange<T>[] ReadIndexedListChanges<T>()
        where T : IDeserializableFromMessage<T>, new()
        {
            return ReadIndexedListChanges(() => ((new T()).Deserialize(this)));
        }

        public virtual ListChange<T>[] ReadIndexedListChanges<T>(Func<T> ReadChange, Func<T> ReadReset = null)
        {
            if (ReadReset == null)
                ReadReset = ReadChange;

            return ReadListChanges(() =>
                                       {

                                           var changeType = ReadByte();
                                           var change = new ListChange<T>();
                                           switch (changeType)
                                           {
                                               case 0x00:
                                                   change.Operation = ListChangeOperation.Remove;
                                                   change.Index = ReadInt16();
                                                   break;
                                               case 0x01:
                                                   change.Operation = ListChangeOperation.Add;
                                                   change.Index = ReadInt16();
                                                   change.Value = ReadChange();
                                                   break;
                                               case 0x02:
                                                   change.Operation = ListChangeOperation.Change;
                                                   change.Value = ReadChange();
                                                   break;
                                               case 0x03:
                                                   change.Operation = ListChangeOperation.ResetAll;
                                                   var size = ReadInt16();
                                                   change.Values = new T[size];
                                                   for (int j = 0; j < size; j++)
                                                   {
                                                       change.Values[j] = ReadReset();
                                                   }
                                                   break;
                                               case 0x04:
                                                   change.Operation = ListChangeOperation.ClearAll;
                                                   break;
                                           }

                                           return change;
                                       });

        }
        
        public virtual ListChange<int>[] ReadIntIndexedListChanges()
        {
            return ReadIndexedListChanges(ReadInt32);
        }


        public virtual ListChange<long>[] ReadLongIndexedListChanges()
        {
            return ReadIndexedListChanges(ReadInt64);
        } 

        public virtual ListChange<float>[] ReadFloatIndexListChanges()
        {
            return ReadIndexedListChanges(ReadFloat);
        } 


        
        public virtual ListChange<T>[] ReadNonIndexListChanges<T>(Func<T> ReadChange)
        {
            return ReadListChanges(() =>
            {

                var changeType = ReadByte();
                var change = new ListChange<T>();
                switch (changeType)
                {

                    case 0x00:
                        change.Operation = ListChangeOperation.Add;
                        change.Value = ReadChange();
                        break;
                    case 0x01:
                        change.Operation = ListChangeOperation.Remove;
                        change.Value = ReadChange();
                        break;
                    case 0x03:
                        change.Operation = ListChangeOperation.ClearAll;
                        break;
                }

                return change;
            });
        }

        public virtual ListChange<T>[] ReadNonIndexListChanges<T>()
            where T: IDeserializableFromMessage<T>, new()
        {
            return ReadNonIndexListChanges(() => ((new T()).Deserialize(this)));
        }

        
        public virtual ListChange<int>[] ReadIntNonIndexListChanges()
        {
            return ReadNonIndexListChanges(ReadInt32);
        }

        public virtual ListChange<long>[] ReadLongNonIndexListChanges()
        {
            return ReadNonIndexListChanges(ReadInt64);
        }

        public virtual ListChange<float>[] ReadFloatNonIndexListChanges()
        {
            return ReadNonIndexListChanges(ReadFloat);
        }

        public virtual ListChange<string>[] ReadStringNonIndexListChanges(Encoding Encoding)
        {
            return ReadNonIndexListChanges(() => ReadString(Encoding));
        }

        public virtual ListChange<string>[] ReadAsciiStringNonIndexListChanges()
        {
            return ReadStringNonIndexListChanges(Encoding.ASCII);
        }

        public virtual ListChange<string>[] ReadUTF8StringNonIndexListChanges()
        {
            return ReadStringNonIndexListChanges(Encoding.UTF8);
        }


        public virtual T[] ReadList<T>(Func<T> CreateObjectFunc, bool HasUpdateCounter = true, bool ReadUpdateType = false)
        {
            var size = ReadInt32();
            //update counter, not useful here
            if (HasUpdateCounter)
            {
                ReadInt32();
            }

            var arr = new T[size];

            for (int i = 0; i < size; i++)
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
                where T : IDeserializableFromMessage<T>, new()
        {

            return ReadList<T>(() => ((new T()).Deserialize(this)), HasUpdateCounter, ReadUpdateType);
        }
    }
}
