using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone.Intangible
{
    [RegisterDeltaMessage(MessageOp.ITNO,0x03)]
    public class IntangibleObjectDeltaMessage3 : DeltaMessage
    {

        public float? Complexity { get; set; }
        public string STFFile { get; set; }
        public string STFName { get; set; }
        public string CustomName { get; set; }
        public int? Volume { get; set; }
        public int? GenericInt { get; set; }

        public IntangibleObjectDeltaMessage3() { }

        public IntangibleObjectDeltaMessage3(byte[] Data, int Size, bool parseFromData = false)
            : base(Data, Size, parseFromData)
        {

        }

        public IntangibleObjectDeltaMessage3(Message message, bool parseFromData = false)
                : base(message.Data, message.Size, parseFromData) {}


        public override bool ParseFromData()
        {
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
                            STFFile = ReadString(Encoding.ASCII);
                            ReadInt32(); //spacer
                            STFName = ReadString(Encoding.ASCII);
                            break;
                        case 0x02:
                            CustomName = ReadString(Encoding.UTF8);
                            break;
                        case 0x03:
                            Volume = ReadInt32();
                            break;
                        case 0x04:
                            GenericInt = ReadInt32();
                            break;
                    }
                }

                return true;
            }
        }

    }
}
