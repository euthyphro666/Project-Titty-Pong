using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TittyPong.Graphics;

namespace TittyPong.Domain
{
    public abstract class Entity
    {
        protected Rectangle body;
        protected Texture2D sprite;
        protected Vector2 position;
        protected Vector2 velocity;

        public float X
        {
            get
            {
                return position.X;
            }
            set
            {
                position.X = value;
                body.X = (int)value;
            }
        }
        public float Y
        {
            get
            {
                return position.Y;
            }
            set
            {
                position.Y = value;
                body.Y = (int)value;
            }
        }
        public float Dx
        {
            get
            {
                return velocity.X;
            }
            set
            {
                velocity.X = value;
            }
        }
        public float Dy
        {
            get
            {
                return velocity.Y;
            }
            set
            {
                velocity.Y = value;
            }
        }

        public Entity(Texture2D spr, float x, float y, int w, int h)
        {
            sprite = spr;
            body = new Rectangle((int)x, (int)y, w, h);
            position = new Vector2(x, y);
            velocity = new Vector2();
        }

        public bool IsCollided(Entity e)
        {
            return body.Intersects(e.body);
        }

        public void Render(ScreenManager screen)
        {
            screen.Render(sprite, body);
        }

    }
}
