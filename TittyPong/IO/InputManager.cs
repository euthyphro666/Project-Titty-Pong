using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TittyPong.Events;

namespace TittyPong.IO
{
    public class InputManager
    {
        
        /// <summary>
        /// List of controls with each index corelating to a player of that index + 1.
        /// ie Index 0 = Player 1, etc.
        /// </summary>
        private Controls[] PlayerControls;
        private PlayerIndex KeyboardPlayer;

        private EventManager events;

        public InputManager(EventManager ev)
        {
            events = ev;
            PlayerControls = new Controls[4];
        }

        public void Init()
        {
            var hasBeenKeyboardPlayer = false;
            foreach (var player in Enum.GetValues(typeof(PlayerIndex)).Cast<PlayerIndex>())
            {
                var isGamepad = GamePad.GetState(player).IsConnected;
                PlayerControls[(int)player] = new Controls(isGamepad, player);
                if (!hasBeenKeyboardPlayer && !isGamepad)
                {
                    hasBeenKeyboardPlayer = true;
                    KeyboardPlayer = player;
                }
            }
        }


        public void Update(GameTime delta)
        {

        }
    }
}
