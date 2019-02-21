using Common.ECS;
using Common.ECS.Systems;
using Common.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TittyGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Master : Game
    {
        private Engine GameEngine;
        private Screen Screen;

        public Master()
        {
            Content.RootDirectory = "Content";

            //This must be created in the constructor of the monogame game root.
            Screen = new Screen(this);
        }
        
        protected override void Initialize()
        {
            base.Initialize();
            IsMouseVisible = true;
            LoadEngine();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        private void LoadEngine()
        {
            GameEngine = new Engine();
            GameEngine.AddSystem(new InputSystem(), false)
                      .AddSystem(new CollisionSystem(), false)
                      .AddSystem(new MovementSystem(), false)
                      .AddSystem(new RenderSystem(Screen), true);
        }    

        protected override void Update(GameTime delta)
        {
            base.Update(delta);
            GameEngine.Update();
        }

        protected override void Draw(GameTime delta)
        {
            base.Draw(delta);
            GameEngine.Render();
        }

    }
}
