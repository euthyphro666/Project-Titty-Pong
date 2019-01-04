using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TittyPong.Graphics;
using TittyPong.IO;



namespace TittyPong.Core
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Master : Game
    {
        
        internal static Input IM { private set; get; }
        internal static Screen SM { private set; get; }

        public Master()
        {
            Content.RootDirectory = "Content";

            SM = new Screen();
            IM = new Input();

            SM.Init(this);
            IM.Init();
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            SM.LoadContent(Content);
        }
        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //Run state update loop here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SM.Start();
            //Run state render loop here
            SM.Stop();

            base.Draw(gameTime);
        }
    }
}
