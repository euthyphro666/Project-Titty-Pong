using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using TittyPong.Contracts;
using TittyPong.Events;
using TittyPong.Events.Args;
using TittyPong.Graphics;
using TittyPong.IO;

namespace TittyPong.Core
{
    public class StateManager : IManager
    {
        private ContentManager Assets;
        private EventManager Events;

        public TittyMenu Menu;
        public TittyGame Game;
        public TittyLoading Load;

        public IManager State;

        public StateManager(ContentManager assets, EventManager events)
        {
            Assets = assets;
            Events = events;

            Menu = new TittyMenu(Assets, Events);
            State = Menu;

            Events.JoinRoomEvent += HandleJoinRoomEvent;
        }

        private void HandleJoinRoomEvent(object sender, JoinRoomEventArgs e)
        {
            Game = new TittyGame(Assets, Events, e.RoomId);
            SwitchState(States.Game);
        }

        public void SwitchState(States state)
        {
            switch (state)
            {
                case States.Menu:
                    State = Menu;
                    break;
                case States.Loading:
                    State = new TittyLoading(Assets, Events);
                    break;
                case States.Game:
                    State = Game;
                    break;
            }
        }

        public void Update(GameTime delta, InputManager input)
        {
            State?.Update(delta, input);
        }

        public void Render(GameTime delta, ScreenManager screen)
        {
            State?.Render(delta, screen);
        }
    }
    public enum States
    {
        Menu,
        Loading,
        Game
    }
}
