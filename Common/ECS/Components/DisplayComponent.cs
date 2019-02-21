using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ECS.Contracts;

namespace Common.ECS.Components
{
    public class DisplayComponent : IComponent
    {
        public Texture2D Sprite { get; set; }
    }
}