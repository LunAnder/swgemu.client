using System;
using System.Text;
using SWG.Client.Utils;
using SWG.Client.Utils.Attribute;


namespace SWG.Client.Network.Messages.Login
{
    [RegisterMessage(MessageOp.LoginClientToken)]
    public class LoginClientTokenMessage : Message
    {
        public Int32 SessionKeySize { get; set; }
        public byte[] SessionKey { get; set; }
        public UInt32 UserID { get; set; }
        public string UserName { get; set; }

        public LoginClientTokenMessage()
        {
            
        }

        public LoginClientTokenMessage(Message ToCreateFrom, bool ParseFromData = false) 
            :base(ToCreateFrom.Data,ToCreateFrom.Size,ParseFromData)
        {
        }


        public override bool ParseFromData()
        {
            ReadIndex = 6;
            SessionKeySize = ReadInt32();

            SessionKey = new byte[SessionKeySize];
            Array.ConstrainedCopy(Data, ReadIndex,SessionKey,0,SessionKeySize);
            ReadIndex += SessionKeySize;
            UserID = ReadUInt32();
            UserName = ReadString(Encoding.ASCII);

            return true;
        }

    }
}
