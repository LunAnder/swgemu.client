using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Abstracts;
using SWG.Client.Utils;

namespace SWG.Client.Network
{
    public abstract class DataContainerReadBase : DataContainerBase, SWG.Client.Network.IDataContainerRead
    {
        private int _readIndex;

        public int ReadIndex { get { return _readIndex; } set { _readIndex = value; } }
        
        public virtual byte PeekByte()
        {
            return Data.PeekByte(ReadIndex);
        }

        public virtual sbyte PeekSByte()
        {
            return Data.PeekSByte(ReadIndex);
        }

        public virtual UInt16 PeekUInt16()
        {
            return Data.PeekUInt16(ReadIndex);
        }
        public virtual Int16 PeekInt16()
        {
            return Data.PeekInt16(ReadIndex);
        }
        public virtual UInt32 PeekUInt32()
        {
            return Data.PeekUInt32(ReadIndex);
        }
        public virtual Int32 PeekInt32()
        {
            return Data.PeekInt32(ReadIndex);
        }
        public virtual UInt64 PeekUInt64()
        {
            return Data.PeekUInt64(ReadIndex);
        }
        public virtual Int64 PeekInt64()
        {
            return Data.PeekInt64(ReadIndex);
        }
        public virtual float PeekFloat()
        {
            return Data.PeekFloat(ReadIndex);
        }
        public virtual double PeekDouble()
        {
            return Data.PeekDouble(ReadIndex);
        }

        public virtual byte ReadByte()
        {
            return Data.ReadByte(ref _readIndex);
        }

        public virtual sbyte ReadSByte()
        {
            return Data.ReadSByte(ref _readIndex);
        }

        public virtual UInt16 ReadUInt16()
        {
            return Data.ReadUInt16(ref _readIndex);
        }
        public virtual Int16 ReadInt16()
        {
            return Data.ReadInt16(ref _readIndex);
        }
        public virtual UInt32 ReadUInt32()
        {
            return Data.ReadUInt32(ref _readIndex);
        }
        public virtual Int32 ReadInt32()
        {
            return Data.ReadInt32(ref _readIndex);
        }
        public virtual ulong ReadUInt64()
        {
            return Data.ReadUInt64(ref _readIndex);
        }
        public virtual Int64 ReadInt64()
        {
            return Data.ReadInt64(ref _readIndex);
        }
        public virtual float ReadFloat()
        {
            return Data.ReadFloat(ref _readIndex);
        }
        public virtual double ReadDouble()
        {
            return Data.ReadDouble(ref _readIndex);
        }

        public virtual string PeekString(Encoding TextEncoding) {
            int lengthBitSize;            
            int length;

            if(TextEncoding.EncodingName.Contains("Unicode") || TextEncoding.EncodingName.Contains("UTF")) {
                lengthBitSize = sizeof(Int32);
                length = PeekInt32();
            }
            else {
                lengthBitSize = sizeof(Int16);
                length = Convert.ToInt32(PeekInt16());
            }

            return TextEncoding.GetString(Data, ReadIndex + lengthBitSize, length);
        }

        public string ReadString(Encoding TextEncoding)
        {
            int charMultiplier;            
            int length;

            if(TextEncoding.EncodingName.Contains("Unicode") || TextEncoding.EncodingName.Contains("UTF")) {
                length = ReadInt32();
                charMultiplier = 2;
            }
            else {
                length = Convert.ToInt32(ReadInt16());
                charMultiplier = 1;
            }

            var stringData = TextEncoding.GetString(Data, ReadIndex, length);
            ReadIndex += length * charMultiplier;
            return stringData;
        }

        public IDisposable TemporaryRead()
        {
            return new TemporaryReadDisposeable(this);
        }

        public void SetReadIntForwardBy(int count = 1)
        {
            ReadIndex += sizeof(int) * count;
        }

        public void SetReadLongForwardBy(int count = 1)
        {
            ReadIndex += sizeof(long) * count;
        }
        public void SetReadShortForwardBy(int count = 1)
        {
            ReadIndex += sizeof(short) * count;
        }

        public void SetReadFloatForwardBy(int count = 1)
        {
            ReadIndex += sizeof(float) * count;
        }

        public void SetReadByteForwardBy(int count = 1)
        {
            ReadIndex += sizeof(byte) * count;
        }
    }
}
