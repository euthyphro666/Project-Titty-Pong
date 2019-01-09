using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TittyPong.Graphics;
using TittyPong.IO;

namespace TittyPong.Contracts
{
    public interface IManager
    {
        void Update(GameTime delta, InputManager input);
        void Render(GameTime delta, ScreenManager screen);
    }
}
