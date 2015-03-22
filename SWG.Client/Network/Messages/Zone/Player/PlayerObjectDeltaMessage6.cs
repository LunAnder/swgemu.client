using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network;
using SWG.Client.Network.Messages;
using SWG.Client.Utils;
using SWG.Client.Utils.Attribute;


namespace SWG.Client.Network.Messages.Zone.Player
{
    [RegisterDeltaMessage(MessageOp.PLAY, 0x06)]
    public class PlayerObjectDeltaMessage6 : DeltaMessage
    {
        public int? RegionId { get; set; }
        public byte? Tag { get; set; }

        public PlayerObjectDeltaMessage6() { }

        public PlayerObjectDeltaMessage6(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
                return false;

            for (int i = 0; i < UpdateCount; i++)
            {
                var updateId = ReadInt16();
                switch (updateId)
                {
                    case 0x00:
                        RegionId = ReadInt32();
                        break;
                    case 0x01:
                        Tag = ReadByte();
                        break;
                }
            }


            return true;
        }
    }
}
