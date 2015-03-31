using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Utils;

namespace SWG.Client.Network.Messages.Zone.Guild
{
    [RegisterBaselineMessage(MessageOp.GILD, 0x06)]
    public class GuildObjectMessage6 : BaselineMessage
    {
        public GuildObjectMessage6()
        {
        }

        public GuildObjectMessage6(byte[] Data, int Size, bool parseFromData = false) : base(Data, Size, parseFromData)
        {
        }

        public GuildObjectMessage6(Message message, bool parseFromData = false) : base(message, parseFromData)
        {
        }
    }
}
