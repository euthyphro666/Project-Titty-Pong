using Common.ECS;
using Common.ECS.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TittyGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Master : Game
    {

        private ContentManager assets;
        private Engine GameEngine;

        public Master()
        {
            Content.RootDirectory = "Content";
            assets = Content;
        }
        
        protected override void Initialize()
        {
            base.Initialize();
            IsMouseVisible = true;
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
            GameEngine.AddSystem(new InputSystem(), false);
            GameEngine.AddSystem(new CollisionSystem(), false);
            GameEngine.AddSystem(new MovementSystem(), false);
            GameEngine.AddSystem(new RenderSystem(), true);
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
