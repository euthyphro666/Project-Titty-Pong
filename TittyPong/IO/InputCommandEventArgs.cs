using Microsoft.Xna.Framework;
using System;

namespace TittyPong.IO
{

    public class InputCommandEventArgs : EventArgs
    {
        public InputCommand Command;
        public bool State;

        public InputCommandEventArgs(InputCommand command, bool state)
        {
            Command = command;
            State = state;
        }
    }
}
