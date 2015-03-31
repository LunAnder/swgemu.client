using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network;
using SWG.Client.Network.Messages;

namespace SWG.Client.Object.Chat
{
    public class ChatRoomUser : IDeserializableFromMessage<ChatRoomUser>
    {
        public string Application { get; set; }
        public string Server { get; set; }
        public string UserName { get; set; }

        public ChatRoomUser Deserialize(IDataContainerRead DataContainer)
        {
            Application = DataContainer.ReadString(Encoding.ASCII);
            Server = DataContainer.ReadString(Encoding.ASCII);
            UserName = DataContainer.ReadString(Encoding.ASCII);
            return this;
        }
    }
}
