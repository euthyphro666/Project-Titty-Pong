using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TittyPong.Domain
{
    public class Nippy : Entity
    {
        public bool Diretion = false;
        public Nippy(Texture2D img, float x, float y, int w, int h) : base(img, x, y, w, h)
        {

        }

    }
}
