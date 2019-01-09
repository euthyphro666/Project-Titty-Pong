namespace Common.Messages
{
    public class ConnectionRequest
    {
        public static MessageIds MessageId => MessageIds.ConnectionRequest;
        public string ClientId { get; set; }
    }
    
    
}