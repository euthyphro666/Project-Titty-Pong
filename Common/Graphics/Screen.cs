using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Graphics
{
    public class Screen
    {
        private GraphicsDeviceManager GraphicsManager;
        private SpriteBatch Batch;
        private bool ContextOpen;

        private Texture2D WhitePixel;
        private SpriteFont DefaultFont;
        private Color Background;

        private Vector2 RenderVect;
        private Rectangle RenderRect;

        public Screen(Game context)
        {
            GraphicsManager = new GraphicsDeviceManager(context)
            {
                GraphicsProfile = GraphicsProfile.Reach,
                //IsFullScreen = true,
                PreferMultiSampling = true,
                PreferredBackBufferHeight = 1080,
                PreferredBackBufferWidth = 1920,
                SynchronizeWithVerticalRetrace = true
            };
        }

        public void Init()
        {
            Batch = new SpriteBatch(GraphicsManager.GraphicsDevice);
            Background = Color.Black;
            WhitePixel = CreatePixelTex(Color.White);
            RenderVect = new Vector2();
            RenderRect = new Rectangle();
        }

        private Texture2D CreatePixelTex(Color col)
        {
            var tex = new Texture2D(GraphicsManager.GraphicsDevice, 1, 1);
            tex.SetData(new Color[] { col });
            return tex;
        }

        public void Start()
        {
            GraphicsManager.GraphicsDevice.Clear(Background);
            Batch.Begin();
            ContextOpen = true;
        }

        public void Stop()
        {
            ContextOpen = false;
            Batch.End();
        }

        public void Render(Texture2D sprite, Vector2 pos)
        {
            if (ContextOpen)
                Batch.Draw(sprite, pos, Color.White);
        }

        public void Render(Texture2D sprite, Rectangle body)
        {
            if (ContextOpen)
                Batch.Draw(sprite, body, Color.White);
        }

        public void Render(Texture2D sprite, Rectangle body, Color col)
        {
            if (ContextOpen)
                Batch.Draw(sprite, body, col);
        }

        public void Render(Texture2D sprite, float x, float y)
        {
            if (!ContextOpen)
                return;
            RenderVect.X = x - (sprite.Width / 2);
            RenderVect.Y = y - (sprite.Height / 2);
            Render(sprite, RenderVect);
        }

        public void Render(Texture2D sprite, float x, float y, float w, float h)
        {
            if (!ContextOpen)
                return;
            RenderRect.X = (int)(x - (w / 2));
            RenderRect.Y = (int)(y - (h / 2));
            RenderRect.Width = (int)w;
            RenderRect.Height = (int)h;
            Render(sprite, RenderRect);
        }

        public void Render(Texture2D sprite, float x, float y, float w, float h, Color col)
        {
            if (!ContextOpen)
                return;
            RenderRect.X = (int)(x - (w / 2));
            RenderRect.Y = (int)(y - (h / 2));
            RenderRect.Width = (int)w;
            RenderRect.Height = (int)h;
            Render(sprite, RenderRect, col);
        }


        public void RenderText(string text, Vector2 pos, Color col)
        {
            if (ContextOpen)
                Batch.DrawString(DefaultFont, text, pos, col);
        }
    }
}
