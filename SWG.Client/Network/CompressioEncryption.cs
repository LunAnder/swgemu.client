using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ionic.Zlib;

namespace SWG.Client.Network
{
    public class CompressioEncryption
    {
        private ZlibCodec _zlib = new ZlibCodec();

        private static readonly UInt32[] CRCTABLE = new [] {
            0x00000000u, 0x77073096u, 0xee0e612cu, 0x990951bau, 0x076dc419u, 0x706af48fu,
            0xe963a535u, 0x9e6495a3u, 0x0edb8832u, 0x79dcb8a4u, 0xe0d5e91eu, 0x97d2d988u,
            0x09b64c2bu, 0x7eb17cbdu, 0xe7b82d07u, 0x90bf1d91u, 0x1db71064u, 0x6ab020f2u,
            0xf3b97148u, 0x84be41deu, 0x1adad47du, 0x6ddde4ebu, 0xf4d4b551u, 0x83d385c7u,
            0x136c9856u, 0x646ba8c0u, 0xfd62f97au, 0x8a65c9ecu, 0x14015c4fu, 0x63066cd9u,
            0xfa0f3d63u, 0x8d080df5u, 0x3b6e20c8u, 0x4c69105eu, 0xd56041e4u, 0xa2677172u,
            0x3c03e4d1u, 0x4b04d447u, 0xd20d85fdu, 0xa50ab56bu, 0x35b5a8fau, 0x42b2986cu,
            0xdbbbc9d6u, 0xacbcf940u, 0x32d86ce3u, 0x45df5c75u, 0xdcd60dcfu, 0xabd13d59u,
            0x26d930acu, 0x51de003au, 0xc8d75180u, 0xbfd06116u, 0x21b4f4b5u, 0x56b3c423u,
            0xcfba9599u, 0xb8bda50fu, 0x2802b89eu, 0x5f058808u, 0xc60cd9b2u, 0xb10be924u,
            0x2f6f7c87u, 0x58684c11u, 0xc1611dabu, 0xb6662d3du, 0x76dc4190u, 0x01db7106u,
            0x98d220bcu, 0xefd5102au, 0x71b18589u, 0x06b6b51fu, 0x9fbfe4a5u, 0xe8b8d433u,
            0x7807c9a2u, 0x0f00f934u, 0x9609a88eu, 0xe10e9818u, 0x7f6a0dbbu, 0x086d3d2du,
            0x91646c97u, 0xe6635c01u, 0x6b6b51f4u, 0x1c6c6162u, 0x856530d8u, 0xf262004eu,
            0x6c0695edu, 0x1b01a57bu, 0x8208f4c1u, 0xf50fc457u, 0x65b0d9c6u, 0x12b7e950u,
            0x8bbeb8eau, 0xfcb9887cu, 0x62dd1ddfu, 0x15da2d49u, 0x8cd37cf3u, 0xfbd44c65u,
            0x4db26158u, 0x3ab551ceu, 0xa3bc0074u, 0xd4bb30e2u, 0x4adfa541u, 0x3dd895d7u,
            0xa4d1c46du, 0xd3d6f4fbu, 0x4369e96au, 0x346ed9fcu, 0xad678846u, 0xda60b8d0u,
            0x44042d73u, 0x33031de5u, 0xaa0a4c5fu, 0xdd0d7cc9u, 0x5005713cu, 0x270241aau,
            0xbe0b1010u, 0xc90c2086u, 0x5768b525u, 0x206f85b3u, 0xb966d409u, 0xce61e49fu,
            0x5edef90eu, 0x29d9c998u, 0xb0d09822u, 0xc7d7a8b4u, 0x59b33d17u, 0x2eb40d81u,
            0xb7bd5c3bu, 0xc0ba6cadu, 0xedb88320u, 0x9abfb3b6u, 0x03b6e20cu, 0x74b1d29au,
            0xead54739u, 0x9dd277afu, 0x04db2615u, 0x73dc1683u, 0xe3630b12u, 0x94643b84u,
            0x0d6d6a3eu, 0x7a6a5aa8u, 0xe40ecf0bu, 0x9309ff9du, 0x0a00ae27u, 0x7d079eb1u,
            0xf00f9344u, 0x8708a3d2u, 0x1e01f268u, 0x6906c2feu, 0xf762575du, 0x806567cbu,
            0x196c3671u, 0x6e6b06e7u, 0xfed41b76u, 0x89d32be0u, 0x10da7a5au, 0x67dd4accu,
            0xf9b9df6fu, 0x8ebeeff9u, 0x17b7be43u, 0x60b08ed5u, 0xd6d6a3e8u, 0xa1d1937eu,
            0x38d8c2c4u, 0x4fdff252u, 0xd1bb67f1u, 0xa6bc5767u, 0x3fb506ddu, 0x48b2364bu,
            0xd80d2bdau, 0xaf0a1b4cu, 0x36034af6u, 0x41047a60u, 0xdf60efc3u, 0xa867df55u,
            0x316e8eefu, 0x4669be79u, 0xcb61b38cu, 0xbc66831au, 0x256fd2a0u, 0x5268e236u,
            0xcc0c7795u, 0xbb0b4703u, 0x220216b9u, 0x5505262fu, 0xc5ba3bbeu, 0xb2bd0b28u,
            0x2bb45a92u, 0x5cb36a04u, 0xc2d7ffa7u, 0xb5d0cf31u, 0x2cd99e8bu, 0x5bdeae1du,
            0x9b64c2b0u, 0xec63f226u, 0x756aa39cu, 0x026d930au, 0x9c0906a9u, 0xeb0e363fu,
            0x72076785u, 0x05005713u, 0x95bf4a82u, 0xe2b87a14u, 0x7bb12baeu, 0x0cb61b38u,
            0x92d28e9bu, 0xe5d5be0du, 0x7cdcefb7u, 0x0bdbdf21u, 0x86d3d2d4u, 0xf1d4e242u,
            0x68ddb3f8u, 0x1fda836eu, 0x81be16cdu, 0xf6b9265bu, 0x6fb077e1u, 0x18b74777u,
            0x88085ae6u, 0xff0f6a70u, 0x66063bcau, 0x11010b5cu, 0x8f659effu, 0xf862ae69u,
            0x616bffd3u, 0x166ccf45u, 0xa00ae278u, 0xd70dd2eeu, 0x4e048354u, 0x3903b3c2u,
            0xa7672661u, 0xd06016f7u, 0x4969474du, 0x3e6e77dbu, 0xaed16a4au, 0xd9d65adcu,
            0x40df0b66u, 0x37d83bf0u, 0xa9bcae53u, 0xdebb9ec5u, 0x47b2cf7fu, 0x30b5ffe9u,
            0xbdbdf21cu, 0xcabac28au, 0x53b39330u, 0x24b4a3a6u, 0xbad03605u, 0xcdd70693u,
            0x54de5729u, 0x23d967bfu, 0xb3667a2eu, 0xc4614ab8u, 0x5d681b02u, 0x2a6f2b94u,
            0xb40bbe37u, 0xc30c8ea1u, 0x5a05df1bu, 0x2d02ef8du
        };
        
        public void Encrypt(byte[] Data, Int32 length, UInt32 Seed, Int32 StartIndex = 0)
        {
            Encrypt(Data, Convert.ToUInt32(length), Seed, Convert.ToUInt32(StartIndex));
        }

        public void Encrypt(byte[] Data, UInt32 length, UInt32 Seed, UInt32 StartIndex = 0)
        {
            int retVal = 0;

            UInt32 blockCount = length / 4;
            UInt32 byteCount = length % 4;
            UInt32 offset = 0;
            
            //encrypt the blocks of 4 bytes
            for (UInt32 count = 0; count < blockCount; count++)
            {
                offset = count * 4 + StartIndex;
                UInt32 cur = BitConverter.ToUInt32(Data, Convert.ToInt32(offset));
                cur ^= Seed;
                
                var convertedBytes = BitConverter.GetBytes(cur);
                Data[offset] = convertedBytes[0];
                Data[offset + 1] = convertedBytes[1];
                Data[offset + 2] = convertedBytes[2];
                Data[offset + 3] = convertedBytes[3];

                Seed = cur;

            }

            //encrypt the remaining 1 to 3 bytes
            for (UInt32 count = blockCount * 4 + StartIndex; count < blockCount * 4 + byteCount + StartIndex; count++)
            {
                Data[count] = (byte)(Data[count] ^ Seed);
            }
        }

        public void Decrypt(byte[] Data, Int32 length, UInt32 Seed, Int32 StartIndex = 0)
        {
            Decrypt(Data, Convert.ToUInt32(length), Seed, Convert.ToUInt32(StartIndex));
        }
        public void Decrypt(byte[] Data, UInt32 length, UInt32 Seed, UInt32 StartIndex = 0)
        {
            UInt32 tempSeed = 0;
            UInt32 blockCount = length / 4;
            UInt32 byteCount = length % 4;
            UInt32 offset = 0;
            
            //decrypt the blocks
            for (UInt32 count = 0; count < blockCount; count++)
            {
                offset = count * 4 + StartIndex;
                tempSeed = BitConverter.ToUInt32(Data, Convert.ToInt32(offset));
                var data =  tempSeed ^ Seed;

                var convertedBytes = BitConverter.GetBytes(data);
                Data[offset] = convertedBytes[0];
                Data[offset + 1] = convertedBytes[1];
                Data[offset + 2] = convertedBytes[2];
                Data[offset + 3] = convertedBytes[3];

                Seed = tempSeed;
            }

            //decrypt the remaining 1 to 3 bytes
            for (UInt32 count = blockCount * 4 + StartIndex; count < blockCount * 4 + byteCount + StartIndex; count++)
            {
                Data[count] = (byte)(Data[count] ^ Seed);
            }
        }

        public UInt32 GenerateCRC(byte[] Data, Int32 Length, UInt32 Seed, Int32 StartIndex = 0)
        {
            return GenerateCRC(Data, Convert.ToUInt32(Length), Seed, Convert.ToUInt32(StartIndex));
        }

        public UInt32 GenerateCRC(byte[] Data, UInt32 Length, UInt32 Seed, UInt32 StartIndex = 0)
        {
            UInt32 crc = 0;
            UInt32 index = 0;

            crc = CRCTABLE[(~Seed) & 0xFF];
            crc ^= 0x00FFFFFF;

            index = (Seed >> 8) ^ crc;
            crc = (crc >> 8) & 0x00FFFFFF;
            crc ^= CRCTABLE[index & 0xFF];

            index = (Seed >> 16) ^ crc;
            crc = (crc >> 8) & 0x00FFFFFF;
            crc ^= CRCTABLE[index & 0xFF];

            index = (Seed >> 24) ^ crc;
            crc = (crc >> 8) & 0x00FFFFFF;
            crc ^= CRCTABLE[index & 0xFF];

            for (UInt32 i = StartIndex; i < Length; i++)
            {
                index = (Data[i]) ^ crc;
                crc = (crc >> 8) & 0x00FFFFFF;
                crc ^= CRCTABLE[index & 0xFF];
            }
                        
            return ~crc;
        }

        public Int32 Compress(byte[] InData, Int32 InLength, byte[] OutData, Int32 OutLength, Int32 InStartIndex = 0,
                              Int32 OutStartIndex = 0)
        {
            return Compress(InData,
                            Convert.ToUInt32(InLength),
                            OutData,
                            Convert.ToUInt32(OutLength),
                            Convert.ToUInt32(InStartIndex),
                            Convert.ToUInt32(OutStartIndex));
        }

        public Int32 Compress(byte[] InData, UInt32 InLength, byte[] OutData, UInt32 OutLength, UInt32 InStartIndex = 0, UInt32 OutStartIndex = 0)
        {
            _zlib.InitializeDeflate(CompressionLevel.Level6, true);

            _zlib.AvailableBytesIn = Convert.ToInt32(InLength);
            _zlib.InputBuffer = InData;
            _zlib.OutputBuffer = OutData;
            _zlib.AvailableBytesOut = Convert.ToInt32(OutLength);
            _zlib.NextIn = Convert.ToInt32(InStartIndex);
            _zlib.NextOut = Convert.ToInt32(OutStartIndex);

            _zlib.Deflate(FlushType.Finish);

            var outBytes = Convert.ToInt32(_zlib.TotalBytesOut);


            if (outBytes != 0)
            {
                _zlib.EndDeflate();
            }

            if (outBytes > InLength)
            {
               Console.WriteLine("WARN compress is inflating size. returning 0");
                return 0;
            }

            return outBytes;
        }

        public Int32 Decompress(byte[] InData, Int32 InLength, byte[] OutData, Int32 OutLength,
                                Int32 InStartIndex = 0, Int32 OutStartIndex = 0)
        {
            return Decompress(InData,
                              Convert.ToUInt32(InLength),
                              OutData,
                              Convert.ToUInt32(OutLength),
                              Convert.ToUInt32(InStartIndex),
                              Convert.ToUInt32(OutStartIndex));
        }

        public Int32 Decompress(byte[] InData, UInt32 InLength, byte[] OutData, UInt32 OutLength, UInt32 InStartIndex = 0, UInt32 OutStartIndex = 0)
        {
            if (InData[0 + InStartIndex] != 120 /*'x'*/)
            {
                return 0;
            }

            _zlib.InitializeInflate(true);

            _zlib.AvailableBytesIn = Convert.ToInt32(InLength);
            _zlib.InputBuffer = InData;
            _zlib.OutputBuffer = OutData;
            _zlib.AvailableBytesOut = Convert.ToInt32(OutLength);
            _zlib.NextIn = Convert.ToInt32(InStartIndex);
            _zlib.NextOut = Convert.ToInt32(OutStartIndex);

            _zlib.Inflate(FlushType.Finish);
            
            int outBytes = Convert.ToInt32(_zlib.TotalBytesOut);

            if (outBytes != 0)
            {
                _zlib.EndInflate();
            }

            return outBytes;
        }
    }
}