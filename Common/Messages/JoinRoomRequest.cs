using System;

namespace Common.Messages
{
    public class JoinRoomRequest
    {
        public static CommunicationMessageIds MessageId => CommunicationMessageIds.JoinRoomRequest;
        public Guid RoomId { get; set; }
    }
}