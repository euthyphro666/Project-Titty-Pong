namespace Common.Messages
{
    public class StartGameRequest
    {
        public static CommunicationMessageIds CommunicationMessageId => CommunicationMessageIds.StartGameRequest;
        public string TargetClientMac { get; set; }
        public string RequestingClientDisplayName { get; set; }
        public string RequestingClientMac { get; set; }
    }
}