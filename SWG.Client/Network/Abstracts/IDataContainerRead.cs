using System;
using System.Security.Policy;
using System.Text;



namespace SWG.Client.Network
{
    public interface IDataContainerRead : IDataContainer
    {
        
        byte PeekByte();
        double PeekDouble();
        float PeekFloat();
        short PeekInt16();
        int PeekInt32();
        long PeekInt64();
        sbyte PeekSByte();
        ushort PeekUInt16();
        uint PeekUInt32();
        ulong PeekUInt64();
        byte ReadByte();
        double ReadDouble();
        float ReadFloat();
        int ReadIndex { get; set; }
        short ReadInt16();
        int ReadInt32();
        long ReadInt64();
        sbyte ReadSByte();
        ushort ReadUInt16();
        uint ReadUInt32();
        ulong ReadUInt64();
        string ReadString(Encoding Encoding);

        IDisposable TemporaryRead();

    }
}
