using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils;

namespace SWG.Client.Network
{
    public abstract class DataContainerWriteBase : DataContainerBase, IDataContainerWrite
    {
        private int _writeIndex;

        public int WriteIndex { get { return _writeIndex; } set { _writeIndex = value; } }
        public int MaxPayload { get; set; }

        public virtual void AddData(byte[] Source, int SourceIndex = 0, int Length = 0 )
        {
            if (Source.Length + WriteIndex >= MaxPayload)
            {
                throw new IndexOutOfRangeException("Data would exceed the max packet size");
            }

            Data.AddData(Source, ref _writeIndex, SourceIndex);
        }

        public virtual void AddData(byte ToAdd)
        {
            Data.AddData(ToAdd, ref _writeIndex);
        }

        public virtual void AddData(sbyte ToAdd)
        {
            Data.AddData(ToAdd, ref _writeIndex);
        }

        public virtual void AddData(UInt16 ToAdd)
        {
            AddData(BitConverter.GetBytes(ToAdd));
        }

        public virtual void AddData(Int16 ToAdd)
        {
            AddData(BitConverter.GetBytes(ToAdd));
        }

        public virtual void AddData(UInt32 ToAdd)
        {
            AddData(BitConverter.GetBytes(ToAdd));
        }

        public virtual void AddData(Int32 ToAdd)
        {
            AddData(BitConverter.GetBytes(ToAdd));
        }

        public virtual void AddData(UInt64 ToAdd)
        {
            AddData(BitConverter.GetBytes(ToAdd));
        }

        public virtual void AddData(Int64 ToAdd)
        {
            AddData(BitConverter.GetBytes(ToAdd));
        }

        public virtual void AddData(float ToAdd)
        {
            AddData(BitConverter.GetBytes(ToAdd));
        }

        public virtual void AddData(double ToAdd)
        {
            AddData(BitConverter.GetBytes(ToAdd));
        }

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
    }
}
