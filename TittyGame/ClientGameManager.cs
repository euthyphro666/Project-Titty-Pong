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

        public Master()
        {
            Content.RootDirectory = "Content";
            assets = Content;
        }



        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime delta)
        {

            base.Update(delta);
        }

        protected override void Draw(GameTime delta)
        {

            base.Draw(delta);
        }

    }
}
