using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Utils
{
    public static class ByteArrayExtension
    {
        public static byte PeekByte(this byte[] Data, int ReadIndex)
        {
            return Data[ReadIndex];
        }

        public static sbyte PeekSByte(this byte[] Data, int ReadIndex)
        {
            return Convert.ToSByte(Data[ReadIndex]);
        }

        public static UInt16 PeekUInt16(this byte[] Data, int ReadIndex)
        {
            return BitConverter.ToUInt16(Data, ReadIndex);
        }
        public static Int16 PeekInt16(this byte[] Data, int ReadIndex)
        {
            return BitConverter.ToInt16(Data, ReadIndex);
        }
        public static UInt32 PeekUInt32(this byte[] Data, int ReadIndex)
        {
            return BitConverter.ToUInt32(Data, ReadIndex);
        }
        public static Int32 PeekInt32(this byte[] Data, int ReadIndex)
        {
            return BitConverter.ToInt32(Data, ReadIndex);
        }
        public static UInt64 PeekUInt64(this byte[] Data, int ReadIndex)
        {
            return BitConverter.ToUInt64(Data, ReadIndex);
        }
        public static Int64 PeekInt64(this byte[] Data, int ReadIndex)
        {
            return BitConverter.ToInt64(Data, ReadIndex);
        }
        public static float PeekFloat(this byte[] Data, int ReadIndex)
        {
            return BitConverter.ToSingle(Data, ReadIndex);
        }
        public static double PeekDouble(this byte[] Data, int ReadIndex)
        {
            return BitConverter.ToDouble(Data, ReadIndex);
        }

        public static UInt16 PeekNetworkUInt16(this byte[] Data, ref int ReadIndex)
        {
            UInt16 data = BitConverter.ToUInt16(Data, ReadIndex);
            //return (UInt16)System.Net.IPAddress.NetworkToHostOrder((short)data);
            return SwapUInt16(data);
        }
        
        public static Int16 PeekNetworkInt16(this byte[] Data, ref int ReadIndex)
        {
            var data = BitConverter.ToInt16(Data, ReadIndex);
            //return System.Net.IPAddress.NetworkToHostOrder(data);
            return SwapInt16(data);
        }
        
        public static UInt32 PeekNetworkUInt32(this byte[] Data, ref int ReadIndex)
        {
            var data = BitConverter.ToUInt32(Data, ReadIndex);
            /*return ((data & 0x000000ff) << 24) +
                   ((data & 0x0000ff00) << 8) +
                   ((data & 0x00ff0000) >> 8) +
                   ((data & 0xff000000) >> 24);*/
            //return (UInt32)System.Net.IPAddress.NetworkToHostOrder(data);
            return SwapUInt32(data);
        }


        public static Int32 PeekNetworkInt32(this byte[] Data, ref int ReadIndex)
        {
            var data = BitConverter.ToInt32(Data, ReadIndex);
            /*return ((data & 0x000000ff) << 24) +
                   ((data & 0x0000ff00) << 8) +
                   ((data & 0x00ff0000) >> 8) +
                   ((data & 0xff000000) >> 24);*/
            //return System.Net.IPAddress.NetworkToHostOrder(data);
            return SwapInt32(data);
        }


        public static byte ReadByte(this byte[] Data, ref int ReadIndex)
        {
            var data = Data[ReadIndex];
            ReadIndex += sizeof(byte);
            return data;
        }

        public static sbyte ReadSByte(this byte[] Data, ref int ReadIndex)
        {
            var data = Convert.ToSByte(Data[ReadIndex]);
            ReadIndex += sizeof(sbyte);
            return data;
        }

        public static UInt16 ReadUInt16(this byte[] Data, ref int ReadIndex)
        {
            var data = BitConverter.ToUInt16(Data, ReadIndex);
            ReadIndex += sizeof(UInt16);
            return data;
        }

        public static UInt16 ReadNetworkUInt16(this byte[] Data, ref int ReadIndex)
        {
            UInt16 data = BitConverter.ToUInt16(Data, ReadIndex);
            ReadIndex += sizeof(UInt16);
            //return (UInt16)System.Net.IPAddress.NetworkToHostOrder((short)data);
            return SwapUInt16(data);
        }

        public static Int16 ReadInt16(this byte[] Data, ref int ReadIndex)
        {
            var data = BitConverter.ToInt16(Data, ReadIndex);
            ReadIndex += sizeof(Int16);
            return data;
        }

        public static Int16 ReadNetworkInt16(this byte[] Data, ref int ReadIndex)
        {
            var data = BitConverter.ToInt16(Data, ReadIndex);
            ReadIndex += sizeof(UInt16);
            //return System.Net.IPAddress.NetworkToHostOrder(data);
            return SwapInt16(data);
        }

        public static UInt32 ReadUInt32(this byte[] Data, ref int ReadIndex)
        {
            var data = BitConverter.ToUInt32(Data, ReadIndex);
            ReadIndex += sizeof(UInt32);
            return data;
        }

        public static UInt32 ReadNetworkUInt32(this byte[] Data, ref int ReadIndex)
        {
            var data = BitConverter.ToUInt32(Data, ReadIndex);
            ReadIndex += sizeof(UInt32);
            /*return ((data & 0x000000ff) << 24) +
                   ((data & 0x0000ff00) << 8) +
                   ((data & 0x00ff0000) >> 8) +
                   ((data & 0xff000000) >> 24);*/
            //return (UInt32)System.Net.IPAddress.NetworkToHostOrder(data);
            return SwapUInt32(data);
        }

        public static Int32 ReadNetworkInt32(this byte[] Data, ref int ReadIndex)
        {
            var data = BitConverter.ToInt32(Data, ReadIndex);
            ReadIndex += sizeof(UInt32);
            /*return ((data & 0x000000ff) << 24) +
                   ((data & 0x0000ff00) << 8) +
                   ((data & 0x00ff0000) >> 8) +
                   ((data & 0xff000000) >> 24);*/
            //return System.Net.IPAddress.NetworkToHostOrder(data);
            return SwapInt32(data);
        }

        public static UInt32 SwapUInt32(UInt32 data)
        {
            
            return (uint)(((SwapUInt16((ushort)data) & 0xffff) << 0x10) |
                           (SwapUInt16((ushort)(data >> 0x10)) & 0xffff));
        }

        public static Int32 SwapInt32(Int32 data)
        {
            return (int)(((SwapInt16((short)data) & 0xffff) << 0x10) |
                          (SwapInt16((short)(data >> 0x10)) & 0xffff));
        }

        public static short SwapInt16(short v)
        {
            return (short)(((v & 0xff) << 8) | ((v >> 8) & 0xff));

        }

         public static ushort SwapUInt16(ushort v)
        {
            return (ushort)(((v & 0xff) << 8) | ((v >> 8) & 0xff));
        }


        public static Int32 ReadInt32(this byte[] Data, ref int ReadIndex)
        {
            var data = BitConverter.ToInt32(Data, ReadIndex);
            ReadIndex += sizeof(Int32);
            return data;
        }
        public static UInt64 ReadUInt64(this byte[] Data, ref int ReadIndex)
        {
            var data = BitConverter.ToUInt64(Data, ReadIndex);
            ReadIndex += sizeof(UInt64);
            return data;
        }
        public static UInt64 ReadNetworkUInt64(this byte[] Data, ref int ReadIndex)
        {
            var data = BitConverter.ToUInt64(Data, ReadIndex);
            ReadIndex += sizeof(UInt64);
            return ((0x00000000000000FF) & (data >> 56)
                    | (0x000000000000FF00) & (data >> 40)
                    | (0x0000000000FF0000) & (data >> 24)
                    | (0x00000000FF000000) & (data >> 8)
                    | (0x000000FF00000000) & (data << 8)
                    | (0x0000FF0000000000) & (data << 24)
                    | (0x00FF000000000000) & (data << 40)
                    | (0xFF00000000000000) & (data << 56));
        }

        public static Int64 ReadInt64(this byte[] Data, ref int ReadIndex)
        {
            var data = BitConverter.ToInt64(Data, ReadIndex);
            ReadIndex += sizeof(Int64);
            return data;
        }
        public static float ReadFloat(this byte[] Data, ref int ReadIndex)
        {
            var data = BitConverter.ToSingle(Data, ReadIndex);
            ReadIndex += sizeof(float);
            return data;
        }
        public static double ReadDouble(this byte[] Data, ref int ReadIndex)
        {
            var data = BitConverter.ToDouble(Data, ReadIndex);
            ReadIndex += sizeof(double);
            return data;
        }

        /// <summary>
        /// Reads a null terminated string from the byte data
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="ReadIndex"></param>
        /// <returns></returns>
        public static string ReadAsciiString(this byte[] Data, ref int ReadIndex)
        {
            var blrd = new StringBuilder();

#pragma warning disable "CS1717"
            for (ReadIndex = ReadIndex; ReadIndex < Data.Length; ReadIndex++)
#pragma warning restore "CS1717"
            {
                char chr = (char)Data[ReadIndex];
                if (chr == '\0')
                {
                    break;
                }

                blrd.Append(chr);
            }

            return blrd.ToString();
        }


        public static void AddData(this byte[] Data, byte[] Source, ref int WriteIndex, int SourceIndex = 0, int Length = 0)
        {
            if (Length == 0)
            {
                Length = Source.Length - SourceIndex;
            }

            Array.ConstrainedCopy(Source, SourceIndex, Data, WriteIndex, Length);
            WriteIndex += Length;
        }

        public static void AddData(this byte[] Data, byte ToAdd, ref int WriteIndex)
        {
            Data[WriteIndex] = ToAdd;
            WriteIndex += sizeof(byte);
        }

        public static void AddData(this byte[] Data, sbyte ToAdd, ref int WriteIndex)
        {
            Data[WriteIndex] = Convert.ToByte(ToAdd);
            WriteIndex += sizeof(sbyte);
        }

        public static void AddData(this byte[] Data, UInt16 ToAdd, ref int WriteIndex)
        {
            AddData(Data, BitConverter.GetBytes(ToAdd), ref WriteIndex);
        }

        public static void AddData(this byte[] Data, Int16 ToAdd, ref int WriteIndex)
        {
            AddData(Data, BitConverter.GetBytes(ToAdd), ref WriteIndex);
        }

        public static void AddData(this byte[] Data, UInt32 ToAdd, ref int WriteIndex)
        {
            AddData(Data, BitConverter.GetBytes(ToAdd), ref WriteIndex);
        }

        public static void AddData(this byte[] Data, Int32 ToAdd, ref int WriteIndex)
        {
            AddData(Data, BitConverter.GetBytes(ToAdd), ref WriteIndex);
        }

        public static void AddData(this byte[] Data, ulong ToAdd, int WriteIndex)
        {
            AddData(Data, BitConverter.GetBytes(ToAdd), ref WriteIndex);
        }

        public static void AddData(this byte[] Data, Int64 ToAdd, ref int WriteIndex)
        {
            AddData(Data, BitConverter.GetBytes(ToAdd), ref WriteIndex);
        }

        /*public static void AddData(this byte[] Data, float ToAdd, ref int WriteIndex)
        {
            AddData(Data, BitConverter.GetBytes(ToAdd), ref WriteIndex);
        }

        public static void AddData(this byte[] Data, double ToAdd, ref int WriteIndex)
        {
            AddData(Data, BitConverter.GetBytes(ToAdd), ref WriteIndex);
        }*/

        public static void AddNetworkData(this byte[] Data, byte[] Source, ref int WriteIndex, int SourceIndex = 0, int Length = 0)
        {
            if (Length == 0)
            {
                Length = Source.Length;
            }

            for (int i = Length -1; i >= SourceIndex; i--)
            {
                Data[WriteIndex] = Source[i];
                WriteIndex++;
            }
        }

        public static void AddNetworkData(this byte[] Data, UInt16 ToAdd, ref int WriteIndex)
        {
            AddData(Data, BitConverter.GetBytes(SwapUInt16(ToAdd)), ref WriteIndex);
        }

        public static void AddNetworkData(this byte[] Data, Int16 ToAdd, ref int WriteIndex)
        {
            AddData(Data, BitConverter.GetBytes(SwapInt16(ToAdd)), ref WriteIndex);
        }

        public static void AddNetworkData(this byte[] Data, UInt32 ToAdd, ref int WriteIndex)
        {
            AddData(Data, BitConverter.GetBytes(SwapUInt32(ToAdd)), ref WriteIndex);
        }

        public static void AddNetworkData(this byte[] Data, Int32 ToAdd, ref int WriteIndex)
        {
            AddData(Data, BitConverter.GetBytes(SwapInt32(ToAdd)), ref WriteIndex);
        }

        /*public static void AddNetworkData(this byte[] Data, UInt64 ToAdd, int WriteIndex)
        {
            AddNetworkData(Data, BitConverter.GetBytes(ToAdd), ref WriteIndex);
        }

        public static void AddNetworkData(this byte[] Data, Int64 ToAdd, ref int WriteIndex)
        {
            AddNetworkData(Data, BitConverter.GetBytes(ToAdd), ref WriteIndex);
        }

        public static void AddNetworkData(this byte[] Data, float ToAdd, ref int WriteIndex)
        {
            AddNetworkData(Data, BitConverter.GetBytes(ToAdd), ref WriteIndex);
        }

        public static void AddNetworkData(this byte[] Data, double ToAdd, ref int WriteIndex)
        {
            AddNetworkData(Data, BitConverter.GetBytes(ToAdd), ref WriteIndex);
        }*/
        
    }
}
