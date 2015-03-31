using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone.Scene
{
    [RegisterMessage(MessageOp.SceneDestroyObject)]
    public class SceneObjectDestroyMessage : Message
    {
        public long ObjectId { get; set; }

        #region Ctor

        public SceneObjectDestroyMessage()
        {
        }

        public SceneObjectDestroyMessage(uint Size)
            : base(Size)
        {
        }

        public SceneObjectDestroyMessage(int Size)
            : base(Size)
        {
        }

        public SceneObjectDestroyMessage(byte[] Data, int Size = 0, bool ParseFromData = false)
            : base(Data, Size, ParseFromData)
        {
        }

        public SceneObjectDestroyMessage(byte[] Data, int StartIndex, int Length, int Size = 0, bool ParseFromData = false)
            : base(Data, StartIndex, Length, Size, ParseFromData)
        {
        }

        public SceneObjectDestroyMessage(Message ToCreateFrom, bool ParseFromData = false)
            : base(ToCreateFrom, ParseFromData)
        {
        }

        public SceneObjectDestroyMessage(Message ToCreateFrom, int StartIndex, int Length, bool ParseFromData = false)
            : base(ToCreateFrom, StartIndex, Length, ParseFromData)
        {
        }

        public SceneObjectDestroyMessage(Message ToCreateFrom, int StartIndex, int Length, int Size = 0, bool ParseFromData = false)
            : base(ToCreateFrom, StartIndex, Length, Size, ParseFromData)
        {
        } 
        #endregion

        public override bool ParseFromData()
        {
            ReadIndex = 6;
            ObjectId = ReadInt64();
            return true;
        }
    }
}
