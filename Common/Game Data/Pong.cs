using Microsoft.Xna.Framework;

namespace Common.Game_Data
{
    public class Pong
    {
        public Vector2 Force { get; set; }
        public Vector2 Position { get; set; }

        public Pong() { }
        public Pong(Pong pong)
        {
            Force = new Vector2(pong.Force.X, pong.Force.Y);
            Position = new Vector2(pong.Position.X, pong.Position.Y);
        }
    }
}