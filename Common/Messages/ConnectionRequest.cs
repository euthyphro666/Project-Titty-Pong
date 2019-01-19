namespace Common.Messages
{
    public class ConnectionRequest
    {
        public static CommunicationMessageIds CommunicationMessageId => CommunicationMessageIds.ConnectionRequest;
        public string ClientId { get; set; }
        public string DisplayName { get; set; }
    }
    
    
}