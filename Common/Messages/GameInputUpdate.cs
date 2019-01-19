using System;

namespace Common.Messages
{
    public class GameInputUpdate
    {
        public static RoomMessageIds CommunicationMessageId => RoomMessageIds.GameInputUpdate;
        // Figure this type out later
        public object Input { get; set; }
    }
}