using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TittyPong.Contracts;
using TittyPong.IO;

namespace TittyPong.Core
{
    public class TittyGame : IManager
    {

        private Texture2D Titty;
        private Rectangle Body;

        public TittyGame()
        {
            var assets = Master.Assets;
            Titty = assets.Load<Texture2D>("Titty");
            Body = new Rectangle(0, 0, 64, 64);
            Master.IM.DirectionEvent += OnMoveTitty;
        }

        private void OnMoveTitty(object sender, InputDirectionEventArgs e)
        {
            var speed = 5;
            switch (e.Direction)
            {
                case InputDirection.RightNorth:
                    Body.Y -= speed;
                    break;
                case InputDirection.RightSouth:
                    Body.Y += speed;
                    break;
                case InputDirection.RightEast:
                    Body.X += speed;
                    break;
                case InputDirection.RightWest:
                    Body.X -= speed;
                    break;
            }
        }

        public void Update()
        {

        }

        public void Render()
        {
            Master.SM.Render(Titty, Body);
        }
    }
}
