namespace Common.Networking
{
    public interface INetworkSocket
    {
        void Start(ReceivedCallback callback);
        void Connect(string ip);
        void Send(byte[] data);
        
    }
    
    public delegate void ReceivedCallback(byte[] data);
}