using System;

namespace Common.Messages
{
    public class RoomMessage
    {
        public static CommunicationMessageIds CommunicationMessageId => CommunicationMessageIds.RoomMessage;
        public Guid RoomId { get; set; }
        public RoomMessageIds RoomMessageId { get; set; }
        public object Contents { get; set; }
    }
}