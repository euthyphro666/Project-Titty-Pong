using System;
using Common.Maths;
using Microsoft.Xna.Framework;

namespace Common.Game_Data
{
    public class Pong
    {
        [NonSerialized]
        private Vector2 InvertY = new Vector2(1, -1);
        
        [NonSerialized]
        private Vector2 InvertX = new Vector2(-1, 1);
        
        public Vector2 Force;
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

            if (Body.X  > 1920 || Body.X  < 0)
            {
                Force *= InvertX;
            }

            if (Body.Y > 1080 || Body.Y < 0)
            {
                Force *= InvertY;
            }

            if (Body.IsCollided(clientABody))
            {
                Body.CalculateRebound(clientABody, ref Force);
                
            }
            else if (Body.IsCollided(clientBBody))
            {
               
                Body.CalculateRebound(clientBBody, ref Force);
            }

            var newX = Body.X + Force.X;
            var newY = Body.Y + Force.Y;
            
            Body.Position = new Vector2(newX, newY);
        }
    }
}