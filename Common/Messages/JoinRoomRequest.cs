using System;

namespace Common.Messages
{
    public class JoinRoomRequest
    {
        public static CommunicationMessageIds MessageId => CommunicationMessageIds.JoinRoomRequest;
        public Guid RoomId { get; set; }
        public string ClientA { get; set; }
        public string ClientB { get; set; }
    }
}