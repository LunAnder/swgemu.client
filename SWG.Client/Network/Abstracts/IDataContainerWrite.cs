using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network
{
    public interface IDataContainerWrite
    {
        int MaxPayload { get; set; }
        int WriteIndex { get; set; }

        void AddData(byte ToAdd);
        void AddData(byte[] Source, int SourceIndex = 0, int Length = 0);
        //void AddData(double ToAdd);
        void AddData(short ToAdd);
        void AddData(int ToAdd);
        //void AddData(long ToAdd);
        void AddData(sbyte ToAdd);
        //void AddData(float ToAdd);
        void AddData(ushort ToAdd);
        void AddData(uint ToAdd);
        //void AddData(ulong ToAdd);
    }
}
