namespace SWG.Client.Network.Messages
{
    public interface IDeserializableFromMessage<T>
    {
        T Deserialize(IDataContainerRead DataContainer);
    }
}
