using Microsoft.Xna.Framework;
using System;

namespace TittyPong.IO
{

    public class InputButtonEventArgs : EventArgs
    {
        public PlayerIndex Player;
        public InputButton Button;
        public bool State;

        public InputButtonEventArgs(PlayerIndex player, InputButton button, bool state)
        {
            Player = player;
            Button = button;
            State = state;
        }
    }
}
