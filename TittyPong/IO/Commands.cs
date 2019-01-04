using System.Collections.Generic;

namespace TittyPong.IO
{
    public class Commands
    {


        public Dictionary<InputCommand, bool> Buttons;

        public Commands()
        {
            Buttons = new Dictionary<InputCommand, bool>
            {
                [InputCommand.Debug] = false,
                [InputCommand.Editor] = false,
                [InputCommand.Reload] = false,
                [InputCommand.Save] = false
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
        public int SetCommandReportChange(InputCommand cmd, bool state)
        {
            var oldVal = Buttons[cmd];
            if (oldVal == state)
                return state ? 2 : 0;
            Buttons[cmd] = state;
            return state ? 1 : 3;
        }
    }
}
