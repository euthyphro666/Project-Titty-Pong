using System;
using System.Collections.Generic;

namespace Common.Messages
{
    public class GameInputUpdate
    {
        public static RoomMessageIds MessageId => RoomMessageIds.GameInputUpdate;
        public string ClientId { get; set; }
        public List<InputState> Inputs { get; set; }
    }
}