using Common;
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
using static Common.InputState;

namespace TittyPong.Core
{
    public class TittyGame : IManager
    {
        private ContentManager assets;
        private EventManager events;
        private InputMessenger messenger;
        private GameSession Session;

        private Texture2D Titty;

        private double Accumulator;
        private const double StepTime = 30;

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

            Accumulator = 0.0;
            LastInputState = 0;
        }
        
        public void Update(GameTime delta, InputManager input)
        {
            var dt = delta.ElapsedGameTime.TotalMilliseconds;
            if (dt > 250)
                dt = 250;
            Accumulator += dt;

            while(Accumulator >= StepTime)
            {
                var up = input.IsKeyDown(PlayerIndex.One, Keys.W);
                var down = input.IsKeyDown(PlayerIndex.One, Keys.S);
                var dir = up ? Direction.Up : (down ? Direction.Down : Direction.None);
                Accumulator -= StepTime;

                UpdateClientInput(dir);
                LastInputState += (dir == Direction.None) ? 0 : 1;

                var state = new InputState { State = dir, InputNumber = LastInputState};
                InputStatesSinceServerSync.Enqueue(state);

                if (dir != Direction.None)
                    UpdateServerInput(state);
            }
        }

        private void UpdateClientInput(Direction dir)
        {
            var scale = (dir == Direction.Up) ? -1 : 1;
            Session.GetThisClient().Body.Position += (Vector2.UnitY * SPEED * scale);
            //Guess collision updates
            Session.State.Nipple.Update(Session.State.ClientA.Body, Session.State.ClientB.Body);
        }

        private void UpdateServerInput(InputState state)
        {
            events.OnInputEvent(this, new InputEventArgs { RoomId = Session.RoomId, State = state });
        }


        private void HandleRoomUpdateEvent(object sender, GameStateArgs e)
        {
            var driftAX = e.State.ClientA.Body.X - Session.State.ClientA.Body.X;
            var driftAY = e.State.ClientA.Body.Y - Session.State.ClientA.Body.Y;
            var driftBX = e.State.ClientB.Body.X - Session.State.ClientB.Body.X;
            var driftBY = e.State.ClientB.Body.Y - Session.State.ClientB.Body.Y;

            //Console.WriteLine("V---------------------- New State ----------------------V");
            //Console.WriteLine($"ClientA differs by ({driftAX}, {driftAY})");
            //Console.WriteLine($"ClientB differs by ({driftBX}, {driftBY})");
            //Console.WriteLine($"Client InputNumber is ahead by ({LastInputState - e.State.LastProcessedInputNumber})");

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
                    var scale = (statesArry[i].State == Direction.Up) ? -1 : (statesArry[i].State == Direction.Down) ? 1 : 0;
                    Session.GetThisClient().Body.Position += (Vector2.UnitY * SPEED * scale);
                    Session.State.Nipple.Update(Session.State.ClientA.Body, Session.State.ClientB.Body);
                }
            }
        }

        public void Render(GameTime delta, ScreenManager screen)
        {
            screen.Render(Titty, Session.State.ClientA.Body.Position, 64, 64);
            screen.Render(Titty, Session.State.ClientB.Body.Position, 64, 64);
            screen.Render(Titty, Session.State.Nipple.Body.Position, 16, 16);
        }

        public void Dispose()
        {
            messenger.Stop();
        }
    }
}
