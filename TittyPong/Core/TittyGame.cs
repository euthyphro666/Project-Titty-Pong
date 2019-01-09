using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TittyPong.Contracts;
using TittyPong.Domain;
using TittyPong.Events;
using TittyPong.Graphics;
using TittyPong.IO;

namespace TittyPong.Core
{
    public class TittyGame : IManager
    {
        private ContentManager assets;
        private EventManager events;
        private Boobie[] boobies;

        public TittyGame(ContentManager ass, EventManager ev)
        {
            assets = ass;
            events = ev;

            var titty = assets.Load<Texture2D>("Titty");

            boobies = new Boobie[]
            {
                new Boobie(titty, 100f, 100f),
                new Boobie(titty, 300f, 100f)
            };
        }

        public void Update(GameTime delta, InputManager input)
        {

        }

        public void Render(GameTime delta, ScreenManager screen)
        {
            for (int i = 0; i < boobies.Length; i++)
                boobies[i].Render(screen);
        }
    }
}
