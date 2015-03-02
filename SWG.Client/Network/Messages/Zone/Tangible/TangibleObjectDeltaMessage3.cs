using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network.Messages.Zone.Tangible
{
    public class TangibleObjectDeltaMessage3 : DeltaMessage
    {

        public float? Complexity { get; set; }
        public string STFName { get; set; }
        public string DefaultName { get; set; }
        public string CustomName { get; set; }
        public int? Volume { get; set; }
        public string CusomizationString { get; set; }
        public int? OptionsBitmask { get; set; }
        public int? IncapTimer { get; set; }
        public int? ConditionDamage { get; set; }
        public int? MaxCondition { get; set; }
        public byte? Static { get; set; }    

        public TangibleObjectDeltaMessage3(Message message, bool parseFromData = false)
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
                        Complexity = ReadFloat();
                        break;
                    case 0x01:
                        STFName = ReadString(Encoding.ASCII);
                        ReadInt32();
                        DefaultName = ReadString(Encoding.ASCII);
                        break;
                    case 0x02:
                        CusomizationString = ReadString(Encoding.UTF8);
                        break;
                    case 0x03:
                        Volume = ReadInt32();
                        break;
                    case 0x04:
                        CusomizationString = ReadString(Encoding.ASCII);
                        break;
                    case 0x05:
                        break;
                    case 0x06:
                        OptionsBitmask = ReadInt32();
                        break;
                    case 0x07:
                        IncapTimer = ReadInt32();
                        break;
                    case 0x08:
                        ConditionDamage = ReadInt32();
                        break;
                    case 0x09:
                        MaxCondition = ReadInt32();
                        break;
                    case 0x0A:
                        Static = ReadByte();
                        break;
                }
            }

            return true;
        }
    }
}
