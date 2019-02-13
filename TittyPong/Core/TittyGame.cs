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
        private const double StepTime = 1000 / 33;

        private Queue<InputState> InputStatesSinceServerSync;
        private long NetworkSyncTime;
        private float SPEED = 20f;
        private bool Started;


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

            InputStatesSinceServerSync = new Queue<InputState>();
            messenger = new InputMessenger(events);
            messenger.Start();

            RegisterEvents();
        }

        #region Events
        private void RegisterEvents()
        {
            events.RoomUpdateEvent += HandleRoomUpdateEvent;
            events.GameStartEvent += HandleGameStartEvent;
        }

        private void HandleGameStartEvent(object sender, GameStartArgs e)
        {
            NetworkSyncTime = e.NetworkSyncTime;
            Started = true;
        }
        #endregion

        public void Update(GameTime delta, InputManager input)
        {
            if (!Started)
                return;

            var dt = delta.ElapsedGameTime.TotalMilliseconds;
            if (dt > 250)
                dt = 250;
            Accumulator += dt;
            NetworkSyncTime += (long)dt;

            while(Accumulator >= StepTime)
            {
                Accumulator -= StepTime;
                var up = input.IsKeyDown(PlayerIndex.One, Keys.W);
                var down = input.IsKeyDown(PlayerIndex.One, Keys.S);
                var dir = up ? Direction.Up : (down ? Direction.Down : Direction.None);

                UpdateClientInput(dir);

                var state = new InputState { State = dir, Timestamp = NetworkSyncTime};
                InputStatesSinceServerSync.Enqueue(state);

                if (dir != Direction.None)
                    UpdateServerInput(state);
            }
        }

        private void UpdateClientInput(Direction dir)
        {
            var scale = (dir == Direction.Up) ? -1 : (dir == Direction.Down) ? 1 : 0;
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
            #region Debugging
            //var driftAX = e.State.ClientA.Body.X - Session.State.ClientA.Body.X;
            //var driftAY = e.State.ClientA.Body.Y - Session.State.ClientA.Body.Y;
            //var driftBX = e.State.ClientB.Body.X - Session.State.ClientB.Body.X;
            //var driftBY = e.State.ClientB.Body.Y - Session.State.ClientB.Body.Y;
            //Console.WriteLine("V---------------------- New State ----------------------V");
            //Console.WriteLine($"ClientA differs by ({driftAX}, {driftAY})");
            //Console.WriteLine($"ClientB differs by ({driftBX}, {driftBY})");
            //Console.WriteLine($"Client InputNumber is ahead by ({LastInputState - e.State.LastProcessedInputNumber})");
            #endregion

            var oldState = Session.State;
            Session.State = e.State;
            ApplyOldInput(e.NetworkTimeSync);
            //Todo: apply interpolation
        }

        private void ApplyOldInput(long tick)
        {
            var statesArry = InputStatesSinceServerSync.ToArray();
            var dequeuing = true;
            for(int i = 0; i < statesArry.Length; i++)
            {
                if(dequeuing)
                {
                    if (statesArry[i].Timestamp >= tick)
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
            screen.Render(Titty, Session.State.ClientA.Body);
            screen.Render(Titty, Session.State.ClientB.Body);
            screen.Render(Titty, Session.State.Nipple.Body);
        }

        public void Dispose()
        {
            messenger.Stop();
        }
    }
}
