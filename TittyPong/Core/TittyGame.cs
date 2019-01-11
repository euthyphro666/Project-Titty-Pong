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
        private TittyState state;

        public TittyGame(ContentManager ass, EventManager ev)
        {
            assets = ass;
            events = ev;

            var titty = assets.Load<Texture2D>("Titty");

            state = new TittyState
            {
                Boobies = new Boobie[]
                {
                    new Boobie(titty, 100f, 100f, 64, 64),
                    new Boobie(titty, 1754f, 100f, 64, 64)
                }
            };
        }

        public void Update(GameTime delta, InputManager input)
        {

        }

        public void Render(GameTime delta, ScreenManager screen)
        {
            for (int i = 0; i < state.Boobies.Length; i++)
                state.Boobies[i].Render(screen);
        }
    }
}
