using Common.ECS;
using Common.ECS.Contracts;
using Common.ECS.SystemEvents;
using Common.ECS.Systems;
using Common.Graphics;
using Common.Networking;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TittyServer
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ServerGameManager : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private readonly Screen Screen;

        private Engine SystemManager;
        private readonly ISystemContext SystemContext;
        private bool DebugMode;

        public ServerGameManager(string startArg)
        {
            DebugMode = (startArg == "-debug" || startArg == "-d");

            Content.RootDirectory = "Content";

            Screen = new Screen(this);
            SystemContext = new SystemContext(1920, 1080);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            Screen.Init();

            LoadEngine();
        }

        private void LoadEngine()
        {
            var server = new NetworkServer();
            
            SystemManager = new Engine(SystemContext);
            SystemManager.AddSystem(new RenderSystem(SystemContext, Screen), 1, true)
                .AddSystem(new CollisionSystem(SystemContext), 1, false)
                .AddSystem(new MovementSystem(SystemContext), 2, false)
                .AddSystem(new NetworkSystem(SystemContext, server), 3, false);

            if (DebugMode)
                SystemManager.AddSystem(new RenderSystem(SystemContext, Screen), 1, true);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);

            SystemManager.Update();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SystemManager.Render();

            base.Draw(gameTime);
        }
    }
}