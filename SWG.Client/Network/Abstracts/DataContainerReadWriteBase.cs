using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using SWG.Client.Network.Abstracts;
using SWG.Client.Utils;

namespace SWG.Client.Network
{
    public abstract class DataContainerReadWriteBase: DataContainerBase, IDataContainerWrite, IDataContainerRead
    {
        private int _writeIndex;
        private int _readIndex;

        public int ReadIndex { get { return _readIndex; } set { _readIndex = value; } }
        public int WriteIndex { get { return _writeIndex; } set { _writeIndex = value; } }
        public int MaxPayload { get; set; }

        public byte PeekByte()
        {
            return Data.PeekByte(ReadIndex);
        }

        public sbyte PeekSByte()
        {
            return Data.PeekSByte(ReadIndex);
        }

        public UInt16 PeekUInt16()
        {
            return Data.PeekUInt16(ReadIndex);
        }
        public Int16 PeekInt16()
        {
            return Data.PeekInt16(ReadIndex);
        }
        public UInt32 PeekUInt32()
        {
            return Data.PeekUInt32(ReadIndex);
        }
        public Int32 PeekInt32()
        {
            return Data.PeekInt32(ReadIndex);
        }
        public UInt64 PeekUInt64()
        {
            return Data.PeekUInt64(ReadIndex);
        }
        public Int64 PeekInt64()
        {
            return Data.PeekInt64(ReadIndex);
        }
        public float PeekFloat()
        {
            return Data.PeekFloat(ReadIndex);
        }
        public double PeekDouble()
        {
            return Data.PeekDouble(ReadIndex);
        }

        public Int16 PeekNetworkInt16()
        {
            return Data.PeekNetworkInt16(ref _readIndex);
        }

        public UInt16 PeekNetworkUInt16()
        {
            return Data.PeekNetworkUInt16(ref _readIndex);
        }

        public UInt32 PeekNetworkUInt32()
        {
            return Data.PeekNetworkUInt32(ref _readIndex);
        }

        public UInt32 PeekNetworkInt32()
        {
            return Data.PeekNetworkUInt32(ref _readIndex);
        }
        
        public byte ReadByte()
        {
            return Data.ReadByte(ref _readIndex);
        }

        public sbyte ReadSByte()
        {
            return Data.ReadSByte(ref _readIndex);
        }

        public UInt16 ReadUInt16()
        {
            return Data.ReadUInt16(ref _readIndex);
        }

        public UInt16 ReadNetworkUInt16()
        {
            return Data.ReadNetworkUInt16(ref _readIndex);
        }

        public Int16 ReadInt16()
        {
            return Data.ReadInt16(ref _readIndex);
        }
        public UInt32 ReadUInt32()
        {
            return Data.ReadUInt32(ref _readIndex);
        }

        public UInt32 ReadNetworkUInt32()
        {
            return Data.ReadNetworkUInt32(ref _readIndex);
        }

        public Int32 ReadNetworkInt32()
        {
            return Data.ReadNetworkInt32(ref _readIndex);
        }

        public Int32 ReadInt32()
        {
            return Data.ReadInt32(ref _readIndex);
        }
        public ulong ReadUInt64()
        {
            return Data.ReadUInt64(ref _readIndex);
        }

        public UInt64 ReadNetworkUInt64()
        {
            return Data.ReadNetworkUInt64(ref _readIndex);
        }

        public Int64 ReadInt64()
        {
            return Data.ReadInt64(ref _readIndex);
        }
        public float ReadFloat()
        {
            return Data.ReadFloat(ref _readIndex);
        }
        public double ReadDouble()
        {
            return Data.ReadDouble(ref _readIndex);
        }

        public virtual string PeekString(Encoding TextEncoding)
        {
            int lengthBitSize;
            int length;

            if (TextEncoding.EncodingName.Contains("Unicode") || TextEncoding.EncodingName.Contains("UTF"))
            {
                lengthBitSize = sizeof(Int32);
                length = PeekInt32();
            }
            else
            {
                lengthBitSize = sizeof(Int16);
                length = Convert.ToInt32(PeekInt16());
            }

            return TextEncoding.GetString(Data, ReadIndex + lengthBitSize, length);
        }

        public string ReadString(Encoding TextEncoding)
        {
            int charMultiplier;
            int length;

            if (TextEncoding.EncodingName.Contains("Unicode") || TextEncoding.EncodingName.Contains("UTF"))
            {
                length = ReadInt32();
                charMultiplier = 2;
            }
            else
            {
                length = Convert.ToInt32(ReadInt16());
                charMultiplier = 1;
            }

            var stringData = TextEncoding.GetString(Data, ReadIndex, length * charMultiplier);
            ReadIndex += length * charMultiplier;
            return stringData;
        }

        public IDisposable TemporaryRead()
        {
            return new TemporaryReadDisposeable(this);
        }


        public void AddData(byte[] Source, int SourceIndex = 0, int Length = 0)
        {
            if (Length == 0)
            {
                Length = Source.Length;
            }

            if (MaxPayload != 0 && Length + WriteIndex >= MaxPayload)
            {
                throw new IndexOutOfRangeException("Data would exceed the max packet size");
            }

            Data.AddData(Source, ref _writeIndex, SourceIndex, Length);
        }

        public void AddData(byte ToAdd)
        {
            Data.AddData(ToAdd, ref _writeIndex);
        }

        public void AddData(sbyte ToAdd)
        {
            Data.AddData(ToAdd, ref _writeIndex);
        }

        public void AddData(UInt16 ToAdd)
        {
            AddData(BitConverter.GetBytes(ToAdd));
        }

        public void AddData(Int16 ToAdd)
        {
            AddData(BitConverter.GetBytes(ToAdd));
        }

        public void AddData(UInt32 ToAdd)
        {
            AddData(BitConverter.GetBytes(ToAdd));
        }

        public void AddData(Int32 ToAdd)
        {
            AddData(BitConverter.GetBytes(ToAdd));
        }

        public void AddData(UInt64 ToAdd)
        {
            AddData(BitConverter.GetBytes(ToAdd));
        }

        public void AddData(Int64 ToAdd)
        {
            AddData(BitConverter.GetBytes(ToAdd));
        }

        /*public void AddData(float ToAdd)
        {
            AddData(BitConverter.GetBytes(ToAdd));
        }

        public void AddData(double ToAdd)
        {
            AddData(BitConverter.GetBytes(ToAdd));
        }*/

        public virtual void AddData(string ToAdd, Encoding TextEncoding)
        {
            if (TextEncoding.EncodingName.Contains("Unicode") || TextEncoding.EncodingName.Contains("UTF"))
            {
                AddData(ToAdd.Length);
            }
            else
            {
                AddData(Convert.ToInt16(ToAdd.Length));
            }
            AddData(TextEncoding.GetBytes(ToAdd));
        }

        public void AddNetworkData(byte[] ToAdd, int SourceIndex = 0, int Length = 0)
        {
            if (Length == 0)
            {
                Length = ToAdd.Length;
            }

            if (MaxPayload != 0 && Length + WriteIndex >= MaxPayload)
            {
                throw new IndexOutOfRangeException("Data would exceed the max packet size");
            }

            Data.AddNetworkData(ToAdd, ref _writeIndex, SourceIndex, Length);
        }

        public void AddNetworkData(byte ToAdd)
        {
            AddNetworkData(BitConverter.GetBytes(ToAdd));
        }

        public void AddNetworkData(sbyte ToAdd)
        {
            AddNetworkData(BitConverter.GetBytes(ToAdd));
        }

        public void AddNetworkData(UInt16 ToAdd)
        {
            Data.AddNetworkData(ToAdd, ref _writeIndex);
            //AddNetworkData(BitConverter.GetBytes(ToAdd));
        }

        public void AddNetworkData(Int16 ToAdd)
        {
            Data.AddNetworkData(ToAdd, ref _writeIndex);
            //AddNetworkData(BitConverter.GetBytes(ToAdd));
        }

        public void AddNetworkData(UInt32 ToAdd)
        {
            Data.AddNetworkData(ToAdd, ref _writeIndex);
            //AddNetworkData(BitConverter.GetBytes(ToAdd));
        }

        public void AddNetworkData(Int32 ToAdd)
        {
            Data.AddNetworkData(ToAdd, ref _writeIndex);
            //AddNetworkData(BitConverter.GetBytes(ToAdd));
        }

        public void AddNetworkData(UInt64 ToAdd)
        {
            UInt64 swapped = (UInt64)IPAddress.HostToNetworkOrder((long)ToAdd);

            Data.AddData(BitConverter.GetBytes(swapped), ref _writeIndex);
            //AddNetworkData(BitConverter.GetBytes(ToAdd));
        }

        public void AddNetworkData(Int64 ToAdd)
        {
            Data.AddData(IPAddress.HostToNetworkOrder(ToAdd), ref _writeIndex);
            //AddNetworkData(BitConverter.GetBytes(ToAdd));
        }

        /*public void AddNetworkData(float ToAdd)
        {
            Data.AddNetworkData(ToAdd, ref _writeIndex);
            //AddNetworkData(BitConverter.GetBytes(ToAdd));
        }

        public void AddNetworkData(double ToAdd)
        {
            Data.AddNetworkData(ToAdd, ref _writeIndex);
            //AddNetworkData(BitConverter.GetBytes(ToAdd));
        }*/


        public void SetReadIntForwardBy(int count = 1)
        {
            ReadIndex += sizeof (int)*count;
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
