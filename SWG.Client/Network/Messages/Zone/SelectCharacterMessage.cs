using System;



namespace SWG.Client.Network.Messages.Zone
{
    public class SelectCharacterMessage : Message
    {
        public static ushort OPCODECOUNT = 2;

        public static MessageOp OPCODE = MessageOp.SelectCharacter;

        public long CharacterID { get; set; }

        public SelectCharacterMessage() : base(14)
        {
            CharacterID = -1;
        }


        public override bool AddFieldsToData()
        {
            if (CharacterID == -1)
            {
                throw new ArgumentException("CharacterID cannot be null", "CharacterID");
            }

            MessageOpCodeEnum = OPCODE;
            OpcodeCount = OPCODECOUNT;

            WriteIndex = 6;

            AddData(CharacterID);

            Size = WriteIndex;

            return true;
        }
   
    }
}
