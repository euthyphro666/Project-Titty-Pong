using Common.Maths;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using TittyPong.Events;

namespace TittyPong.Graphics
{
    [Flags]
    public enum Alignment { Center = 0, Left = 1, Right = 2, Top = 4, Bottom = 8 }

    public class ScreenManager
    {
        private GraphicsDeviceManager GDM;
        private SpriteBatch SB;
        private bool ContextOpen;

        private Color Background;
        private SpriteFont Font;

        private Texture2D WhitePixel;

        private EventManager events;

        public ScreenManager(EventManager ev)
        {
            events = ev;
        }

        public void Init(Game game)
        {
            GDM = new GraphicsDeviceManager(game)
            {
                GraphicsProfile = GraphicsProfile.Reach,
                //IsFullScreen = true,
                PreferMultiSampling = true,
                PreferredBackBufferHeight = 1080,
                PreferredBackBufferWidth = 1920,
                SynchronizeWithVerticalRetrace = true
            };
        }
        public void LoadContent(ContentManager content)
        {
            SB = new SpriteBatch(GDM.GraphicsDevice);
            //Font = content.Load<SpriteFont>("Fonts/Consolas");
            Background = Color.Black;

            WhitePixel = CreatePixelTex(Color.White);
        }
        public void Start()
        {
            GDM.GraphicsDevice.Clear(Background);
            SB.Begin();
            ContextOpen = true;
        }
        public void Stop()
        {
            ContextOpen = false;
            SB.End();
        }
        public Texture2D CreatePixelTex(Color col)
        {
            var tex = new Texture2D(GDM.GraphicsDevice, 1, 1);
            tex.SetData(new Color[] { col });
            return tex;
        }
        public float Width()
        {
            return GDM.PreferredBackBufferWidth;
        }
        public float Height()
        {
            return GDM.PreferredBackBufferHeight;
        }

        //Todo: Make this better
        public static Rectangle Body = new Rectangle();
        public void Render(Texture2D sprite, Vector2 pos, int w, int h)
        {
            Body.X = (int)pos.X;
            Body.Y = (int)pos.Y;
            Body.Width = w;
            Body.Height = h;
            if (ContextOpen)
                SB.Draw(sprite, Body, Color.White);
        }
        //TODO: the body should be defined inside the circle and updated given changes to position.
        public void Render(Texture2D sprite, Circle circle)
        {
            Body.X = (int) (circle.Position.X - circle.Radius);
            Body.Y = (int) (circle.Position.Y - circle.Radius);
            Body.Width = (int) circle.Radius * 2;
            Body.Height = (int) circle.Radius * 2;
            if (ContextOpen)
                SB.Draw(sprite, Body, Color.White);
        }
        public void Render(Texture2D sprite, Vector2 pos)
        {
            if (ContextOpen)
                SB.Draw(sprite, pos, Color.White);
        }
        public void Render(Texture2D sprite, Rectangle body)
        {
            if (ContextOpen)
                SB.Draw(sprite, body, Color.White);
        }
        public void RenderText(string text, Vector2 pos, Color col)
        {
            if (ContextOpen)
                SB.DrawString(Font, text, pos, col);
        }

        public void RenderText(string text, Vector2 pos, Color col, Alignment align)
        {
            Vector2 size = Font.MeasureString(text);

            if (align.HasFlag(Alignment.Center))
            {
                pos.X -= size.X / 2;
                pos.Y -= size.Y / 2;
            }
            if (align.HasFlag(Alignment.Right))
                pos.X -= size.X;
            if (align.HasFlag(Alignment.Bottom))
                pos.Y -= size.Y;

            RenderText(text, pos, col);
        }

        public void RenderRectangle(Rectangle area, int width, Color color)
        {
            SB.Draw(WhitePixel, new Rectangle(area.X, area.Y, area.Width, width), color);
            SB.Draw(WhitePixel, new Rectangle(area.X, area.Y, width, area.Height), color);
            SB.Draw(WhitePixel, new Rectangle(area.X + area.Width - width, area.Y, width, area.Height), color);
            SB.Draw(WhitePixel, new Rectangle(area.X, area.Y + area.Height - width, area.Width, width), color);
        }
        public void RenderRectangle(Rectangle area)
        {
            RenderRectangle(area, 1, Color.White);
        }
        public void RenderCircle(Vector2 center, float radius, Color color, int lineWidth = 2, int segments = 16)
        {
            Vector2[] vertex = new Vector2[segments];

            double increment = Math.PI * 2.0 / segments;
            double theta = 0.0;

            for (int i = 0; i < segments; i++)
            {
                vertex[i] = center + radius * new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
                theta += increment;
            }
            RenderPolygon(vertex, segments, color, lineWidth);
        }
        public void RenderPolygon(Vector2[] vertex, int count, Color color, int lineWidth)
        {
            if (count > 0)
            {
                for (int i = 0; i < count - 1; i++)
                {
                    RenderLineSegment(vertex[i], vertex[i + 1], color, lineWidth);
                }
                RenderLineSegment(vertex[count - 1], vertex[0], color, lineWidth);
            }
        }
        public void RenderLineSegment(Vector2 point1, Vector2 point2, Color color, int lineWidth)
        {
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = Vector2.Distance(point1, point2);

            SB.Draw(WhitePixel, point1, null, color,
            angle, Vector2.Zero, new Vector2(length, lineWidth),
            SpriteEffects.None, 0f);
        }


    }
}
