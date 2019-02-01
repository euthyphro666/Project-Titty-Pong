using System;

namespace Common.Messages
{
    public class GameInputUpdate
    {
        public static RoomMessageIds MessageId => RoomMessageIds.GameInputUpdate;
        public string ClientId { get; set; }
        public InputState Input { get; set; }
    }
}