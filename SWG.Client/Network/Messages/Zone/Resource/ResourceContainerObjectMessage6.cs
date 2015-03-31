using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Utils;

namespace SWG.Client.Network.Messages.Zone.Resource
{
    [RegisterBaselineMessage(MessageOp.RCNO, 0x06)]
    public class ResourceContainerObjectMessage6 : BaselineMessage
    {

        public string STFFile { get; set; }
        public string STFName { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public string ResourceType { get; set; }
        public string ResourceName { get; set; }
        
        public ResourceContainerObjectMessage6()
        {
        }

        public ResourceContainerObjectMessage6(byte[] Data, int Size, bool parseFromData = false) 
            : base(Data, Size, parseFromData)
        {
        }

        public ResourceContainerObjectMessage6(Message message, bool parseFromData = false) 
            : base(message, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
            {
                return false;
            }

            STFFile = ReadString(Encoding.ASCII);
            SetReadIntForwardBy(1);
            STFName = ReadString(Encoding.ASCII);
            Name = ReadString(Encoding.UTF8);
            Size = ReadInt32();
            ResourceType = ReadString(Encoding.ASCII);
            ResourceName = ReadString(Encoding.UTF8);

            return true;
        }
    }
}
