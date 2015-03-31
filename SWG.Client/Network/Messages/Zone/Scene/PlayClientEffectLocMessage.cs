using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone.Scene
{
    [RegisterMessage(MessageOp.PlayClientEffectLocMessage)]
    public class PlayClientEffectLocMessage : Message
    {
        public string File { get; set; }
        public string Planet { get; set; }
        public float PositionX { get; set; }
        public float PositionZ { get; set; }
        public float PositionY { get; set; }
        public long Unknown1 { get; set; }
        public int Unknown2 { get; set; }

        #region ctor
        public PlayClientEffectLocMessage()
        {
        }

        public PlayClientEffectLocMessage(uint Size)
            : base(Size)
        {
        }

        public PlayClientEffectLocMessage(int Size)
            : base(Size)
        {
        }

        public PlayClientEffectLocMessage(byte[] Data, int StartIndex, int Length, int Size = 0, bool ParseFromData = false)
            : base(Data, StartIndex, Length, Size, ParseFromData)
        {
        }

        public PlayClientEffectLocMessage(byte[] Data, int Size = 0, bool ParseFromData = false)
            : base(Data, Size, ParseFromData)
        {
        }

        public PlayClientEffectLocMessage(Message ToCreateFrom, bool ParseFromData = false)
            : base(ToCreateFrom, ParseFromData)
        {
        }

        public PlayClientEffectLocMessage(Message ToCreateFrom, int StartIndex, int Length, bool ParseFromData = false)
            : base(ToCreateFrom, StartIndex, Length, ParseFromData)
        {
        }

        public PlayClientEffectLocMessage(Message ToCreateFrom, int StartIndex, int Length, int Size = 0, bool ParseFromData = false)
            : base(ToCreateFrom, StartIndex, Length, Size, ParseFromData)
        {
        } 
        #endregion

        public override bool ParseFromData()
        {
            ReadIndex = 6;
            File = ReadString(Encoding.ASCII);
            Planet = ReadString(Encoding.ASCII);
            PositionX = ReadFloat();
            PositionZ = ReadFloat();
            PositionY = ReadFloat();
            Unknown1 = ReadInt64();
            Unknown2 = ReadInt32();
            return true;
        }
    }
}
