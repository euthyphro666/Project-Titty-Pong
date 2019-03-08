namespace Common.Networking
{
    public interface INetworkSocket
    {
        void Start(ReceivedCallback callback);
        void Connect(string ip);
        void Send(MessageIds msgId, byte[] data);
        
    }
    
    public delegate void ReceivedCallback(MessageIds msgId, byte[] data);
}