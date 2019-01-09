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
    public enum InputButton
    {
        Jump,
        LightAttack,
        HeavyAttack,
        DashLeft,
        DashRight,
        DashUp,
        DashDown
    }
    public enum InputDirection
    {
        LeftNorth,
        LeftSouth,
        LeftEast,
        LeftWest,
        RightNorth,
        RightSouth,
        RightEast,
        RightWest
    }
    public enum InputCommand
    {
        Debug,
        Editor,
        Reload,
        Save
    }
    public class InputManager
    {

        private MouseState LastMouseState;
        private Commands Command;
        /// <summary>
        /// List of controls with each index corelating to a player of that index + 1.
        /// ie Index 0 = Player 1, etc.
        /// </summary>
        private Controls[] PlayerControls;
        private PlayerIndex KeyboardPlayer;
        /// <summary>
        /// Keyboard Button Mapping
        /// </summary>
        private readonly Dictionary<InputButton, Keys> KBM;
        /// <summary>
        /// Keyboard Direction Mapping
        /// </summary>
        private readonly Dictionary<InputDirection, Keys> KDM;

        private EventManager events;

        private readonly Dictionary<InputCommand, Keys> KCM;
        public InputManager(EventManager ev)
        {
            events = ev;

            PlayerControls = new Controls[4];
            KBM = new Dictionary<InputButton, Keys>
            {
                [InputButton.Jump] = Keys.Escape,
                [InputButton.LightAttack] = Keys.Escape,
                [InputButton.HeavyAttack] = Keys.Escape,
                [InputButton.DashLeft] = Keys.Escape,
                [InputButton.DashRight] = Keys.Escape,
                [InputButton.DashUp] = Keys.Escape,
                [InputButton.DashDown] = Keys.Escape
            };
            KDM = new Dictionary<InputDirection, Keys>
            {
                [InputDirection.LeftNorth] = Keys.W,
                [InputDirection.LeftSouth] = Keys.S,
                [InputDirection.LeftEast] = Keys.D,
                [InputDirection.LeftWest] = Keys.A,
                [InputDirection.RightNorth] = Keys.W,
                [InputDirection.RightSouth] = Keys.S,
                [InputDirection.RightEast] = Keys.D,
                [InputDirection.RightWest] = Keys.A
            };
            KCM = new Dictionary<InputCommand, Keys>
            {
                [InputCommand.Debug] = Keys.F1,     //Toggles debugging information
                [InputCommand.Editor] = Keys.F2,    //Toggles editor overlay
                [InputCommand.Reload] = Keys.F3,    //Triggers a reload event
                [InputCommand.Save] = Keys.F4       //Triggers a level save event
            };
        }

        public void Init()
        {
            LastMouseState = Mouse.GetState();
            Command = new Commands();

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
            var kState = Keyboard.GetState();
            var mState = Mouse.GetState();
            var mArgs = new InputMouseEventArgs(mState.X, mState.Y);

            events.OnMouseHoverEvent(this, mArgs);

            //Command events
            foreach (var cmd in Enum.GetValues(typeof(InputCommand)).Cast<InputCommand>())
            {
                var cmdState = kState.IsKeyDown(KCM[cmd]);
                var cr = Command.SetCommandReportChange(cmd, cmdState);
                var args = new InputCommandEventArgs(cmd, cmdState);
                switch (cr)
                {
                    case 3:
                        events.OnCommandEvent(this, args);
                        break;
                }
            }

            //Mouse events
            var lastLeft = LastMouseState.LeftButton == ButtonState.Pressed;
            var left = mState.LeftButton == ButtonState.Pressed;
            if(lastLeft)
            {
                if (left)
                    events.OnMouseHeldEvent(this, mArgs);
                else
                    events.OnMouseUpEvent(this, mArgs);
            }
            else
                if(left)
                    events.OnMouseDownEvent(this, mArgs);
            LastMouseState = mState;

            //Keyboard / gamepad events
            foreach (var player in Enum.GetValues(typeof(PlayerIndex)).Cast<PlayerIndex>())
            {
                var control = PlayerControls[(int)player];
                if (control.UsingGamePad)
                {
                    var gState = GamePad.GetState(player);
                }
                else if (KeyboardPlayer == player)
                {
                    foreach (var btn in Enum.GetValues(typeof(InputButton)).Cast<InputButton>())
                    {
                        var btnState = kState.IsKeyDown(KBM[btn]);
                        var br = control.SetButtonReportChange(btn, btnState);
                        var args = new InputButtonEventArgs(player, btn, btnState);
                        switch (br)
                        {
                            case 1:
                                events.OnButtonDownEvent(this, args);
                                break;
                            case 2:
                                events.OnButtonHeldEvent(this, args);
                                break;
                            case 3:
                                events.OnButtonUpEvent(this, args);
                                break;
                        }
                    }
                    foreach (var dir in Enum.GetValues(typeof(InputDirection)).Cast<InputDirection>())
                    {
                        var value = (kState.IsKeyDown(KDM[dir]) ? 1f : 0f);
                        if (control.SetDirectionReportChange(dir, value))
                            events.OnDirectionEvent(this, new InputDirectionEventArgs(player, dir, value));
                    }
                }
            }
        }
    }
}
