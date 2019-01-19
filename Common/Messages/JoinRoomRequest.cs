using System;

namespace Common.Messages
{
    public class JoinRoomRequest
    {
        public static CommunicationMessageIds CommunicationMessageId => CommunicationMessageIds.JoinRoomRequest;
        public Guid RoomId { get; set; }
    }
}