using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network.Messages.Zone.Tangible
{
    public class TangibleObjectMessage3 : BaselineMessage
    {

        public float Complexity { get; set; }
        public string STFName { get; set; }
        public string DefaultName { get; set; }
        public string CustomName { get; set; }
        public int Volume { get; set; }
        public string CusomizationString { get; set; }
        public int OptionsBitmask { get; set; }
        public int IncapTimer { get; set; }
        public int ConditionDamage { get; set; }
        public int MaxCondition { get; set; }
        public byte Static { get; set; }    

        public TangibleObjectMessage3(byte[] Data, int Size, bool parseFromData = false)
                : base(Data, Size, parseFromData)
        {
            
        }

        public TangibleObjectMessage3(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
                return false;
            Complexity = ReadFloat();
            STFName = ReadString(Encoding.ASCII);
            ReadInt32();
            DefaultName = ReadString(Encoding.ASCII);
            CustomName = ReadString(Encoding.Unicode);
            Volume = ReadInt32();
            CusomizationString = ReadString(Encoding.ASCII);
            ReadInt32();
            ReadInt32();
            OptionsBitmask = ReadInt32();
            IncapTimer = ReadInt32();
            ConditionDamage = ReadInt32();
            MaxCondition = ReadInt32();
            Static = ReadByte();


            return true;
        }
    }
}
