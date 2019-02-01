﻿using Common;
using Common.Game_Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TittyPong.Contracts;
using TittyPong.Domain;
using TittyPong.Events;
using TittyPong.Events.Args;
using TittyPong.Graphics;
using TittyPong.IO;
using TittyPong.NET.Game;

namespace TittyPong.Core
{
    public class TittyGame : IManager
    {
        private ContentManager assets;
        private EventManager events;
        private InputMessenger messenger;
        private GameSession Session;

        private Texture2D Titty;

        private Queue<InputState> InputStatesSinceServerSync;
        private int LastInputState;
        private float SPEED = 20f;


        #region Sound Testing

        SoundEffect TittyFx;

        #endregion

        public TittyGame(ContentManager ass, EventManager ev, GameSession session)
        {
            assets = ass;
            events = ev;
            Session = session;

            Titty = assets.Load<Texture2D>("Titty");
            TittyFx = assets.Load<SoundEffect>("Sounds\\TittyCollision");

            events.RoomUpdateEvent += HandleRoomUpdateEvent;

            InputStatesSinceServerSync = new Queue<InputState>();

            messenger = new InputMessenger(events);
            messenger.Start();

            LastInputState = 0;
        }

        bool ShouldUpdate = false;
        public void Update(GameTime delta, InputManager input)
        {
            ShouldUpdate = !ShouldUpdate;
            if (!ShouldUpdate)
                return;

            var up = input.IsKeyDown(PlayerIndex.One, Keys.W);
            var down = input.IsKeyDown(PlayerIndex.One, Keys.S);
            if (up ^ down)
                HandleNewInputState(up ? InputState.Direction.Up : InputState.Direction.Down);
        }

        private void HandleNewInputState(InputState.Direction dir)
        {
            var scale = (dir == InputState.Direction.Up) ? -1 : 1;
            Session.GetThisClient().Position += (Vector2.UnitY * SPEED * scale);

            var state = new InputState { State = dir, InputNumber = ++LastInputState };
            InputStatesSinceServerSync.Enqueue(state);
            events.OnInputEvent(this, new InputEventArgs { RoomId = Session.RoomId, State = state });
        }


        private void HandleRoomUpdateEvent(object sender, GameStateArgs e)
        {

            var driftAX = e.State.ClientA.Position.X - Session.State.ClientA.Position.X;
            var driftAY = e.State.ClientA.Position.Y - Session.State.ClientA.Position.Y;
            var driftBX = e.State.ClientB.Position.X - Session.State.ClientB.Position.X;
            var driftBY = e.State.ClientB.Position.Y - Session.State.ClientB.Position.Y;

            Console.WriteLine("V---------------------- New State ----------------------V");
            Console.WriteLine($"ClientA differs by ({driftAX}, {driftAY})");
            Console.WriteLine($"ClientB differs by ({driftBX}, {driftBY})");
            Console.WriteLine($"Client InputNumber is ahead by ({LastInputState - e.State.LastProcessedInputNumber})");

            var oldState = Session.State;
            Session.State = e.State;
            ApplyOldInput();
            //Todo: apply interpolation
        }

        private void ApplyOldInput()
        {
            var statesArry = InputStatesSinceServerSync.ToArray();
            var dequeuing = true;
            for(int i = 0; i < statesArry.Length; i++)
            {
                if(dequeuing)
                {
                    if (statesArry[i].InputNumber >= Session.State.LastProcessedInputNumber)
                        dequeuing = false;
                    InputStatesSinceServerSync.Dequeue();
                }
                else
                {
                    var scale = (statesArry[i].State == InputState.Direction.Up) ? -1 : 1;
                    Session.GetThisClient().Position += (Vector2.UnitY * SPEED * scale);
                }
            }
        }

        public void Render(GameTime delta, ScreenManager screen)
        {
            screen.Render(Titty, Session.State.ClientA.Position, 64, 64);
            screen.Render(Titty, Session.State.ClientB.Position, 64, 64);
            screen.Render(Titty, Session.State.Nipple.Position, 16, 16);
        }

        public void Dispose()
        {
            messenger.Stop();
        }
    }
}
