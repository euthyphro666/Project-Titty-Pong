using System;

namespace Common.Game_Data
{
    [Serializable]
    public class GameState
    {
        public int LastProcessedInputNumber { get; set; }
        public Client ClientA { get; set; }
        public Client ClientB { get; set; }
        public Pong Nipple { get; set; }
    }
}