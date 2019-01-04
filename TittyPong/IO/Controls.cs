using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TittyPong.IO
{

    public class Controls
    {

        public Dictionary<InputButton, bool> Buttons;
        public Dictionary<InputDirection, float> Direction;

        public PlayerIndex Player;
        public bool UsingGamePad;

        public Controls(bool usingGamepad, PlayerIndex player)
        {
            Player = player;
            UsingGamePad = usingGamepad;

            Buttons = new Dictionary<InputButton, bool>
            {
                [InputButton.Jump] = false,
                [InputButton.LightAttack] = false,
                [InputButton.HeavyAttack] = false,
                [InputButton.DashLeft] = false,
                [InputButton.DashRight] = false,
                [InputButton.DashUp] = false,
                [InputButton.DashDown] = false
            };
            Direction = new Dictionary<InputDirection, float>
            {
                [InputDirection.LeftNorth] = 0f,
                [InputDirection.LeftSouth] = 0f,
                [InputDirection.LeftEast] = 0f,
                [InputDirection.LeftWest] = 0f,
                [InputDirection.RightNorth] = 0f,
                [InputDirection.RightSouth] = 0f,
                [InputDirection.RightEast] = 0f,
                [InputDirection.RightWest] = 0f
            };
        }
        /// <summary>
        /// Sets the button to the new value and reports whether it was pressed, dragged, released, or none.
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="value"></param>
        /// <returns>
        ///     0 for not pressed, no change
        ///     1 for down
        ///     2 for held
        ///     3 for up
        /// </returns>
        public int SetButtonReportChange(InputButton btn, bool state)
        {
            var oldVal = Buttons[btn];
            if (oldVal == state)
                return state ? 2 : 0;
            Buttons[btn] = state;
            return state ? 1 : 3;
        }
        public bool SetDirectionReportChange(InputDirection dir, float value)
        {
            return (Direction[dir] = value) > 0;
        }
    }
}
