using System;

namespace Common.Messages
{
    public class JoinRoomRequest
    {
        public static CommunicationMessageIds MessageId => CommunicationMessageIds.JoinRoomRequest;
        public Guid RoomId { get; set; }
        public string ClientAId { get; set; }
        public string ClientBId { get; set; }
        
        public string ClientADisplayName { get; set; }
        public string ClientBDisplayName { get; set; }
    }
}