namespace Common.Messages
{
    public class StartGameResponse
    {
        public static MessageIds MessageId => MessageIds.StartGameResponse;
        public bool StartGameAccepted { get; set; }
        public string RespondingClientMac { get; set; }
        public string RequestingClientMac { get; set; }
    }
}