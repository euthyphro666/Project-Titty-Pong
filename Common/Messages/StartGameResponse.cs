namespace Common.Messages
{
    public class StartGameResponse
    {
        public static CommunicationMessageIds CommunicationMessageId => CommunicationMessageIds.StartGameResponse;
        public bool StartGameAccepted { get; set; }
        public string RespondingClientMac { get; set; }
        public string RequestingClientMac { get; set; }
    }
}