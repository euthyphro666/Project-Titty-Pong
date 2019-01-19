using System;

namespace Common.Messages
{
    public class JoinRoomRequest
    {
        public static MessageIds MessageId => MessageIds.JoinRoomRequest;
        public Guid RoomId { get; set; }
    }
}