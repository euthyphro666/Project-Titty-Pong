using Common.Maths;
using Microsoft.Xna.Framework;

namespace Common.Game_Data
{
    public class Pong
    {
        private Vector2 InvertY = new Vector2(1, -1);
        private Vector2 InvertX = new Vector2(-1, 1);
        
        public Vector2 Force { get; set; }
        public Circle Body { get; set; }

        public Pong()
        {
        }

        public Pong(Pong pong)
        {
            Force = new Vector2(pong.Force.X, pong.Force.Y);
            Body = new Circle(pong.Body.Position.X, pong.Body.Position.Y, 8);
        }

        public void Update(Circle clientABody, Circle clientBBody)
        {
            var newX = Body.X + Force.X;
            var newY = Body.Y + Force.Y;

            if (newX > 1920 || newX < 0)
            {
                Force *= InvertX;
                newX = Body.X + Force.X;
            }

            if (newY > 1080 || newY < 0)
            {
                Force *= InvertY;
                
                newY = Body.Y + Force.Y;
            }

            if (Body.IsCollided(clientABody))
            {
                Force *= InvertX;
                Force *= InvertY;
                newY = Body.Y + Force.Y;
                newX = Body.X + Force.X;
            }
            else if (Body.IsCollided(clientBBody))
            {
                
                Force *= InvertX;
                Force *= InvertY;
                newY = Body.Y + Force.Y;
                newX = Body.X + Force.X;
            }

            Body.Position = new Vector2(newX, newY);
        }
    }
}