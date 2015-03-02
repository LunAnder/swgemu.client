namespace SWG.Client.Network.Messages.Zone
{
     public class ClientPermissionsMessage :Message
    {

         public byte GalaxyOpenFlag { get; set; }
         public byte CharacterSlotOpenFlag { get; set; }
         public byte UnlimitedCharacterCreationFlag { get; set; }


         public ClientPermissionsMessage(Message message, bool parseFromData = false)
                 : base(message.Data, message.Size, parseFromData)
         {
             
         }

         public override bool ParseFromData()
         {
             ReadIndex = 6;

             GalaxyOpenFlag = ReadByte();
             CharacterSlotOpenFlag = ReadByte();
             UnlimitedCharacterCreationFlag = ReadByte();
             return true;
         }
    }
}
