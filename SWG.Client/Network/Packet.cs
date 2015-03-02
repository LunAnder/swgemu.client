using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils;

namespace SWG.Client.Network
{
    public class Packet : DataContainerReadWriteBase
    {
        public bool Encrypted { get; set; }
        public bool Compressed { get; set; }
        public bool SendCRC { get; set; }

        public UInt64 TimeCreated { get; set; }
        public UInt64 TimeQueued { get; set; }
        public UInt64 TimeSent { get; set; }
        public UInt64 OOHTimeSent { get; set; }

        public UInt32 Resends { get; set; }
        public UInt32 CRC { get; set; }
        public int Rresends { get; set; }
        public int Size { get; set; }
        
        public Int16 PacketType 
        {
            get
            {
                return BitConverter.ToInt16(Data, 0);
            }
            set
            {
                Array.ConstrainedCopy(BitConverter.GetBytes(value), 0, Data, 0, sizeof(Int16));
            }
        }

        public SessionOp PacetTypeEnum
        {
            get
            {
                return (SessionOp)Convert.ToInt32(PacketType);
            }
            set
            {
                PacketType = Convert.ToInt16((int)value);
            }
        }

        public Packet(UInt16 Size) 
            : this(Convert.ToUInt32(Size))
        {
            
        }

        public Packet(UInt32 Size) 
            : this(Convert.ToInt32(Size))
        {
            
        }
        
        public Packet(int Size) 
            : this(new byte[Size])
        {
            
        }
        

        public Packet(byte[] Data, int StartIndex, int Length)
        {
            Data = new byte[Length];
            Buffer.BlockCopy(Data, StartIndex, this.Data, 0, Length);
            Size = Length;
        }

        public Packet(byte[] Data = null)
        {
            this.Data = Data ?? new byte[496];
            Size = this.Data.Length;
        }      
        
        public void ResizeToWriteIndex()
        {
            ResizeTo(WriteIndex);
        }

        public void Reset()
        {
            TimeCreated = 0;
            TimeSent = 0;
            Resends = 0;
            Size = 0;
            ReadIndex = 0;
            WriteIndex = 0;
            Compressed = false;
            Encrypted = false;
            CRC = 0;
        }

        public override string ToString()
        {
            return string.Format("Packet ({0} - {1})", PacetTypeEnum, Size);
        }
    }
}
