using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone.Object
{
    [ObjectControllerMessage(0x140)]
    public class WeaponRangesMessage : ObjectControllerMessage
    {

        public long WeaponObjectId { get; set; }
        public float IdealRange { get; set; }
        public float MaxRange { get; set; }
        public int PointBlankAccuracy { get; set; }
        public int IdealAccuracy { get; set; }
        public int MaxRangeAccuracy { get; set; }

        public WeaponRangesMessage()
        {
        }

        public WeaponRangesMessage(Message message, bool parseFromData = false)
            : base(message, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
            {
                return false;
            }

            WeaponObjectId = ReadInt64();
            IdealRange = ReadFloat();
            MaxRange = ReadFloat();
            PointBlankAccuracy = ReadInt32();
            IdealAccuracy = ReadInt32();
            MaxRangeAccuracy = ReadInt32();

            return true;
        }
    }
}
