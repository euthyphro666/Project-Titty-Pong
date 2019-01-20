using System;

namespace Common.Messages
{
    public class RoomConfirmation
    {
        public string ClientMac { get; set; }
        public Guid RoomId { get; set; }
    }
}