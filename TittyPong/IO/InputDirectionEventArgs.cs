using System;
using Microsoft.Xna.Framework;

namespace TittyPong.IO
{

    public class InputDirectionEventArgs : EventArgs
    {
        public PlayerIndex Player;
        public InputDirection Direction;
        public float Value;

        public InputDirectionEventArgs(PlayerIndex player, InputDirection direction, float value)
        {
            Player = player;
            Direction = direction;
            Value = value;
        }
    }
}
