using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Network.Objects;
using SWG.Client.Utils;

namespace SWG.Client.Network.Messages.Zone.Guild
{
    [RegisterBaselineMessage(MessageOp.GILD, 0x03)]
    public class GuildObjectMessage3 : BaselineMessage
    {

        public StringFile STFFile { get; set; }
        public string CustomName { get; set; }
        public int Volume { get; set; }
        public string[] GuildIds { get; set; }

        #region ctor

        public GuildObjectMessage3(byte[] Data, int Size, bool parseFromData = false) : base(Data, Size, parseFromData)
        {
        }

        public GuildObjectMessage3(Message message, bool parseFromData = false) : base(message, parseFromData)
        {
        }

        public GuildObjectMessage3()
        {
        }

        #endregion

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
            {
                return false;
            }

            SetReadFloatForwardBy(1);
            STFFile = new StringFile().Deserialize(this);
            CustomName = ReadString(Encoding.UTF8);
            Volume = ReadInt32();
            GuildIds = ReadList(() => ReadString(Encoding.ASCII));
            return true;
        }
    }
}
