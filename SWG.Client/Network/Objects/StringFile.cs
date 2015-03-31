using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Messages;

namespace SWG.Client.Network.Objects
{
    public class StringFile : IDeserializableFromMessage<StringFile>
    {

        public string STFFile { get; set; }
        public string STFName { get; set; }
        public StringFile Deserialize(IDataContainerRead DataContainer)
        {
            STFFile = DataContainer.ReadString(Encoding.ASCII);
            DataContainer.SetReadIntForwardBy(1);
            STFName = DataContainer.ReadString(Encoding.ASCII);

            return this;
        }
    }
}
