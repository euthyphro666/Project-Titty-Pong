namespace Common.Messages
{
    public class Message
    {
        public CommunicationMessageIds CommunicationMessageId { get; set; }
        public object Contents { get; set; }
    }
}