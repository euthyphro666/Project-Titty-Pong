using System;

namespace Common.Game_Data
{
    [Serializable]
    public class GameState
    {
        public Client ClientA { get; set; }
        public Client ClientB { get; set; }
        public Pong Nipple { get; set; }

        public GameState() { }
        public GameState(GameState state)
        {
            ClientA = new Client(state.ClientA);
            ClientB = new Client(state.ClientB);
            Nipple = new Pong(state.Nipple);
        }
    }
}