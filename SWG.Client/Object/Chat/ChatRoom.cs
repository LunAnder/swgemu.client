using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network;
using SWG.Client.Network.Messages;

namespace SWG.Client.Object.Chat
{
    public class ChatRoom : IDeserializableFromMessage<ChatRoom>
    {
        public int RoomId { get; set; }
        public int PrivateFlag { get; set; }
        public byte ModeratedFlag { get; set; }
        public string RoomPathName { get; set; }
        public string OwnerApplicaiton { get; set; }
        public string OwnerServer { get; set; }
        public string Owner { get; set; }
        public string CreatorApplication { get; set; }
        public string CreatorServer { get; set; }
        public string Creator { get; set; }
        public string RoomTitle { get; set; }
        public ChatRoomUser[] Moderators { get; set; }
        public ChatRoomUser[] Users { get; set; }


        public ChatRoom Deserialize(IDataContainerRead DataContainer)
        {

            RoomId = DataContainer.ReadInt32();
            PrivateFlag = DataContainer.ReadInt32();
            ModeratedFlag = DataContainer.ReadByte();
            RoomPathName = DataContainer.ReadString(Encoding.ASCII);
            OwnerApplicaiton = DataContainer.ReadString(Encoding.ASCII);
            OwnerServer = DataContainer.ReadString(Encoding.ASCII);
            Owner = DataContainer.ReadString(Encoding.ASCII);
            CreatorApplication = DataContainer.ReadString(Encoding.ASCII);
            CreatorServer = DataContainer.ReadString(Encoding.ASCII);
            Creator = DataContainer.ReadString(Encoding.ASCII);
            RoomTitle = DataContainer.ReadString(Encoding.UTF8);

            var moderatorListSize = DataContainer.ReadInt32();
            Moderators = new ChatRoomUser[moderatorListSize];
            for (int i = 0; i < moderatorListSize; i++)
            {
                Moderators[i] = new ChatRoomUser().Deserialize(DataContainer);
            }

            var userListSize = DataContainer.ReadInt32();
            Users = new ChatRoomUser[userListSize];
            for (int i = 0; i < moderatorListSize; i++)
            {
                Users[i] = new ChatRoomUser().Deserialize(DataContainer);
            }

            return this;
        }
    }
}
