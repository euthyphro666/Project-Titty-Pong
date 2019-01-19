namespace Common.Messages
{
    public class JoinRoomRequest
    {
        public static MessageIds MessageId => MessageIds.JoinRoomRequest;
        public int RoomId { get; set; }
    }
}