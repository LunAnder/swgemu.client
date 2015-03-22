using SWG.Client.Utils;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone
{
    [RegisterMessage(MessageOp.ClientPermissionsMessage)]
     public class ClientPermissionsMessage :Message
    {

         public byte GalaxyOpenFlag { get; set; }
         public byte CharacterSlotOpenFlag { get; set; }
         public byte UnlimitedCharacterCreationFlag { get; set; }

         public ClientPermissionsMessage() { } 

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
