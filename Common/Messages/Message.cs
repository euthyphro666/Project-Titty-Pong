namespace Common.Messages
{
    public class Message
    {
        public CommunicationMessageIds MessageId { get; set; }
        public object Contents { get; set; }
    }
}