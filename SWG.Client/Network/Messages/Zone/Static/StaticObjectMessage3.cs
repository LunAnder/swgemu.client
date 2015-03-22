using System.Text;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Utils;


namespace SWG.Client.Network.Messages.Zone.Static
{
    [RegisterBaselineMessage(MessageOp.STAO, 0x03)]
    public class StaticObjectMessage3 : BaselineMessage
    {

        public int Complexity { get; set; }
        public string STFFile { get; set; }
        public string STFName { get; set; }
        public string CustomName { get; set; }
        public int Volume { get; set; }

        public StaticObjectMessage3() { }

        public StaticObjectMessage3(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
                return false;

            Complexity = ReadInt32();
            STFFile = ReadString(Encoding.ASCII);
            ReadInt32(); //spacer
            STFName = ReadString(Encoding.ASCII);
            CustomName = ReadString(Encoding.UTF8);
            Volume = ReadInt32();


            return true;
        }
    }
}
