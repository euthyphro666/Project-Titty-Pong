using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Myra;
using System;
using TittyPong.Contracts;
using TittyPong.Events;
using TittyPong.Graphics;
using TittyPong.IO;
using TittyPong.NET;

namespace TittyPong.Core
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Master : Game
    {

        private ContentManager assets;
        private EventManager events;
        private InputManager input;
        private ScreenManager screen;

        private StateManager State;

        private readonly Client MessageClient;
        private readonly MessageConsumer Consumer;
        private readonly MessageProducer Producer;

        public Master()
        {
            Content.RootDirectory = "Content";
            assets = Content;

            events = new EventManager();
            input = new InputManager(events);
            screen = new ScreenManager(events);

            Consumer = new MessageConsumer(events);
            Producer = new MessageProducer(events);
            MessageClient = new Client(events);
            MessageClient.ReceivedMessageEvent += Consumer.ConsumeMessage;

            //Begin debugging
            Logger.Events = events;
            //End debugging

            RegisterEvents();

            screen.Init(this);
            input.Init();
        }

        #region Events
        public void RegisterEvents()
        {
            events.ChangeStateEvent += HandleStateSwitchEvent;
        }
        #endregion


        protected override void Initialize()
        {
            IsMouseVisible = true;
            MyraEnvironment.Game = this;
            State = new StateManager(assets, events);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            screen.LoadContent(Content);

            State.Game = new TittyGame(assets, events, Guid.Empty);
            //State.SwitchState(States.Game);
        }

        protected override void UnloadContent()
        {
            
        }

        public void HandleStateSwitchEvent(object sender, ChangeStateEventArgs e)
        {
            State.SwitchState(e.State);
        }

        protected override void Update(GameTime delta)
        {
            input.Update(delta);
            State.Update(delta, input);
            base.Update(delta);
        }

        protected override void Draw(GameTime delta)
        {
            screen.Start();
            State.Render(delta, screen);
            screen.Stop();
            base.Draw(delta);
        }
        
    }
}
