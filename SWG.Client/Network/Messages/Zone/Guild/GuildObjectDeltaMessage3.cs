using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone.Guild
{
    [RegisterDeltaMessage(MessageOp.GILD, 0x03)]
    public class GuildObjectDeltaMessage3 : DeltaMessage
    {

        public ListChange<string>[] GuildIds { get; set; }

        #region ctor

        public GuildObjectDeltaMessage3(byte[] Data, int Size, bool parseFromData = false)
            : base(Data, Size, parseFromData)
        {
        }

        public GuildObjectDeltaMessage3(Message message, bool parseFromData = false) : base(message, parseFromData)
        {
        }


        public GuildObjectDeltaMessage3()
        {
        }

        #endregion

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
            {
                return false;
            }

            for (int i = 0; i < UpdateCount; i++)
            {
                var updateId = ReadInt16();
                switch (updateId)
                {
                    case 0x04:
                        GuildIds = ReadNonIndexListChanges(() => ReadString(Encoding.ASCII));
                        break;
                }
            }

            return true;
        }
    }
}
