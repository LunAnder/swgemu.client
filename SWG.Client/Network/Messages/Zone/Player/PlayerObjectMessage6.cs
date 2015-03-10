using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network;
using SWG.Client.Network.Messages;
using SWG.Client.Network.Messages.Zone.Creature;
using SWG.Client.Utils;


namespace SWG.Client.Network.Messages.Zone.Player
{
    [RegisterBaselineMessage(MessageOp.PLAY, 0x06)]
    public class PlayerObjectMessage6 : BaselineMessage
    {
        public int RegionId { get; set; }
        public byte Tag { get; set; }

        public PlayerObjectMessage6() { }

        public PlayerObjectMessage6(byte[] Data, int Size = 0, bool parseFromData = false)
            : base(Data, Size, parseFromData)
        {
        }

        public PlayerObjectMessage6(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
                return false;

            RegionId = ReadInt32();
            Tag = ReadByte();


            return true;
        }
    }
}
