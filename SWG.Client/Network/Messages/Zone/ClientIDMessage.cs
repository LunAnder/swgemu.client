using System;



namespace SWG.Client.Network.Messages.Zone
{
    public class ClientIDMessage : Message
    {

        public static ushort OPCODECOUNT = 3;

        public static MessageOp OPCODE = MessageOp.ClientIdMsg;

        public UInt32 UserID { get; set; }
        public byte[] SessionKey { get; set; }

        public ClientIDMessage(UInt32 UserID, byte[] SessionKey)
                : base(14 + SessionKey.Length)
        {
            this.UserID = UserID;
            this.SessionKey = SessionKey;
        }

        public ClientIDMessage()
        {
        }

        public override bool AddFieldsToData()
        {
            OpcodeCount = OPCODECOUNT;
            MessageOpCodeEnum = OPCODE;

            WriteIndex = 6;

            AddData(UserID);
            AddData((int)SessionKey.Length);
            AddData(SessionKey,0,SessionKey.Length);

            SetSizeToWriteIndex();
            return true;
        }
    }
}
