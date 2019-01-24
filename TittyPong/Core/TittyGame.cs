using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        private Guid RoomId;


        #region Sound Testing

        SoundEffect TittyFx;

        #endregion

        public TittyGame(ContentManager ass, EventManager ev, Guid roomId)
        {
            assets = ass;
            events = ev;
            RoomId = roomId;

            var titty = assets.Load<Texture2D>("Titty");

            state = new TittyState
            {
                Boobies = new Titty[]
                {
                    new Titty(titty, 100f, 100f, 64, 64),
                    new Titty(titty, 1754f, 100f, 64, 64)
                },
                Nipple = new Nippy(titty, 929f, 924f, 16, 16)
            };

            TittyFx = assets.Load<SoundEffect>("Sounds\\TittyCollision");
        }

        public void Update(GameTime delta, InputManager input)
        {
            var SPEED = 15;
            var up = input.IsKeyDown(PlayerIndex.One, Keys.W);
            var down = input.IsKeyDown(PlayerIndex.One, Keys.S);
            if (up ^ down)
            {
                var dir = up ? -1 : 1;
                var y = state.Boobies[0].Y + (dir * SPEED);
                state.Boobies[0].Y = Clamp(y, 0, 1080 - 64);
            }

            up = input.IsKeyDown(PlayerIndex.One, Keys.Up);
            down = input.IsKeyDown(PlayerIndex.One, Keys.Down);
            if (up ^ down)
            {
                var dir = up ? -1 : 1;
                var y = state.Boobies[1].Y + (dir * SPEED);
                state.Boobies[1].Y = Clamp(y, 0, 1080 - 64);
            }
            UpdatePaddle();
        }

        private void UpdatePaddle()
        {
            var SPEED = 10;
            var nip = state.Nipple;
            var dir = nip.Diretion ? -1 : 1;
            var x = nip.X + (dir * SPEED);
            nip.X = x;
            if(x > 1920 - 16 || x < 0)
            {
                //Play wall sound
                nip.Diretion = !nip.Diretion;
            }
            if(nip.IsCollided(state.Boobies[0]) || nip.IsCollided(state.Boobies[1]))
            {
                //Play bounce sound
                nip.Diretion = !nip.Diretion;

            }

        }

        private float Clamp(float value, float min, float max)
        {
            return value > min ? ((value > max) ? max : value) : min;
        }


        public void Render(GameTime delta, ScreenManager screen)
        {
            for (int i = 0; i < state.Boobies.Length; i++)
                state.Boobies[i].Render(screen);
            state.Nipple.Render(screen);
        }
    }
}
