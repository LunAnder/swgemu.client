using System.Text;

using SWG.Client.Network.Objects;



namespace SWG.Client.Network.Messages.Login
{
    public class LoginEnumClusterMessage : Message
    {
        public int ServerCount { get; set; }

        public ServerName[] Servers { get; set; }

        public int MaxCharactersPerAccount { get; set; }


        public LoginEnumClusterMessage(Message ToCreateFrom, bool ParseFromData = false) 
            :base(ToCreateFrom.Data,ToCreateFrom.Size,ParseFromData)
        {
        }

        public override bool ParseFromData()
        {
            ReadIndex = 6;

            ServerCount = ReadInt32();

            Servers = new ServerName[ServerCount];


            for (int i = 0; i < ServerCount; i++)
            {
                Servers[i] = new ServerName
                    {
                            ServerID = ReadInt32(),
                            ServerDisplayName = ReadString(Encoding.ASCII),
                            Timezone = ReadInt32(),
                    };
            }

            MaxCharactersPerAccount = ReadInt32();
            return true;
        }
    }
}
