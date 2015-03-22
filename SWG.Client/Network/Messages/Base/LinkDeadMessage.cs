namespace SWG.Client.Network.Messages.Base
{
    public class LinkDeadMessage : Message
    {
        

        public LinkDeadMessage(Message message, bool parseFromData = false)
            : base(message.Data, message.Size, parseFromData)
        {
        }

        public override bool ParseFromData()
        {
            ReadIndex = 6;
            return true;
        }
    }
}
