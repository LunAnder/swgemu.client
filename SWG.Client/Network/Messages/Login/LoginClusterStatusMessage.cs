using System.Text;

using SWG.Client.Network.Objects;
using SWG.Client.Utils;
using SWG.Client.Utils.Attribute;


namespace SWG.Client.Network.Messages.Login
{
    [RegisterMessage(MessageOp.LoginClusterStatus)]
    public class LoginClusterStatusMessage : Message
    {
        public int ServerCount { get; set; }

        public ServerDetails[] Servers { get; set; }

        public LoginClusterStatusMessage() { }

        public LoginClusterStatusMessage(Message ToCreateFrom, bool ParseFromData = false) 
            :base(ToCreateFrom.Data,ToCreateFrom.Size,ParseFromData)
        {
        }

        
        public override bool ParseFromData()
        {
            ReadIndex = 6;

            ServerCount = ReadInt32();

            Servers = new ServerDetails[ServerCount];

            for (int i = 0; i < ServerCount; i++)
            {
                Servers[i] = new ServerDetails
                    {
                            ServerID = ReadInt32(),
                            ServerIP = ReadString(Encoding.ASCII),
                            ServerPort = ReadUInt16(),
                            PingPort = ReadUInt16(),
                            Population = ReadInt32(),
                            MaxCapacity = ReadInt32(),
                            MaxCharsPerServer = ReadInt32(),
                            Distance = ReadInt32(),
                            Status = ReadInt32(),
                            NotRecommended = ReadByte() == 1,
                    };
            }
            return true;
        }
        
    }
}
