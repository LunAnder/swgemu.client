using System.Text;
using SWG.Client.Utils;
using SWG.Client.Network.Objects;



namespace SWG.Client.Network.Messages.Login
{
    [RegisterMessage(MessageOp.EnumerateCharacterId)]
    public class EnumerateCharacterIdMessage : Message
    {
        public int CharacterCount { get; set; }

        public Character[] Characters { get; set; }

        public EnumerateCharacterIdMessage()
        {
            
        }

        public EnumerateCharacterIdMessage(Message ToCreateFrom, bool ParseFromData = false) 
            :base(ToCreateFrom.Data,ToCreateFrom.Size,ParseFromData)
        {
            
        }

        public override bool ParseFromData()
        {
            ReadIndex = 6;

            CharacterCount = ReadInt32();
            Characters = new Character[CharacterCount];

            for (int i = 0; i < CharacterCount; i++)
            {
                Characters[i] = new Character
                    {
                            Name = ReadString(Encoding.Unicode),
                            RaceGenderCRC = ReadInt32(),
                            CharacterID = ReadInt64(),
                            ServerID = ReadInt32(),
                            Status = ReadInt32()
                    };
            }

            return true;
        }

    }
}
