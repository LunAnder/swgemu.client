using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network
{
    public class Message : DataContainerReadWriteBase
    {
        public int Size { get; set; }
        //public int Index { get; set; }
        public UInt64 CreatedTime { get; set; }
        public UInt64 QueuedTime { get; set; }
        public bool FastPath { get; set; }
        public bool Routed { get; set; }
        public byte Priority { get; set; }


        public UInt16 OpcodeCount
        {
            get { return BitConverter.ToUInt16(Data, 0); }

            set { 
                var readCurrent = ReadIndex;
                ReadIndex = 0;
                AddData(value);
                ReadIndex = readCurrent;
            }
        }

        public UInt32 MessageOpCode
        {
            get
            {
                return BitConverter.ToUInt32(Data, 2);
            }
            set
            {
                Array.ConstrainedCopy(BitConverter.GetBytes(value), 0, Data, 2, sizeof(UInt32));
            }
        }

        public MessageOp MessageOpCodeEnum
        {
            get { return (MessageOp)MessageOpCode; }
            set { MessageOpCode = (UInt32)value; }
        }

        public Message(): this(496)
        {
        }

        public Message(UInt32 Size) : this(Convert.ToInt32(Size))
        {
            
        }

        public Message(int Size)
        {
            Data = new byte[Size];
            this.Size = Size;
        }

        public Message(byte[] Data, int Size = 0, bool ParseFromData = false)
        {
            if (Size == 0)
            {
                Size = Data.Length;
            }
            this.Data = Data;
            this.Size = Size;

            if (ParseFromData)
            {
                this.ParseFromData();
            }
        }

        public Message(byte[] Data, int StartIndex, int Length, int Size = 0, bool ParseFromData = false)
        {
            if (Size == 0)
            {
                Size = Length;
            }

            this.Size = Size;
            this.Data = new byte[Size];
            Array.ConstrainedCopy(Data, StartIndex, this.Data, 0, Length);

            if (ParseFromData)
            {
                this.ParseFromData();
            }
        }

        public Message(Message ToCreateFrom, bool ParseFromData = false) 
            :this(ToCreateFrom.Data,ToCreateFrom.Size,ParseFromData)
        {
            
        }

        public Message(Message ToCreateFrom, int StartIndex, int Length, bool ParseFromData = false)
            : this(ToCreateFrom.Data, StartIndex, Length, Length, ParseFromData)
        {

        }

        public Message(Message ToCreateFrom, int StartIndex, int Length, int Size = 0, bool ParseFromData = false)
            : this(ToCreateFrom.Data, StartIndex, Length, Size, ParseFromData)
        {

        }

        public void SetSizeToWriteIndex()
        {
            Size = WriteIndex;
        }

        public virtual bool ParseFromData()
        {
            return false;
        }


        public virtual bool AddFieldsToData()
        {
            return false;
        }

        public override string ToString()
        {
            return string.Format("Message ({0} - {1})", MessageOpCodeEnum, Size);
        }
    }
}
