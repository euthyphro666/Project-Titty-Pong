using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Myra;
using TittyPong.Contracts;
using TittyPong.Events;
using TittyPong.Graphics;
using TittyPong.IO;
using TittyPong.NET;
using TittyPong.UI;

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

        private IManager state;

        private readonly Client MessageClient;
        private readonly MessageConsumer Consumer;

        public Master()
        {
            Content.RootDirectory = "Content";
            assets = Content;

            events = new EventManager();
            input = new InputManager(events);
            screen = new ScreenManager(events);

            Consumer = new MessageConsumer(events);
            MessageClient = new Client(events);
            MessageClient.ReceivedMessageEvent += Consumer.ConsumeMessage;

            screen.Init(this);
            input.Init();
        }


        protected override void Initialize()
        {
            IsMouseVisible = true;
            MyraEnvironment.Game = this;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            screen.LoadContent(Content);

            //Initialize starting state
            //InitializeMenu();
            InitializeGame();
        }

        protected override void UnloadContent()
        {
        }

        private void InitializeMenu()
        {
            state = new TittyMenu(assets, events);
        }

        private void InitializeGame()
        {
            state = new TittyGame(assets, events);
        }

        protected override void Update(GameTime delta)
        {
            input.Update(delta);
            state?.Update(delta, input);
            base.Update(delta);
        }

        protected override void Draw(GameTime delta)
        {
            screen.Start();
            state?.Render(delta, screen);
            screen.Stop();
            base.Draw(delta);
        }
        
    }
}
