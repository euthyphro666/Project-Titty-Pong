using Common.ECS.Contracts;
using Common.ECS.Nodes;
using Common.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ECS.Systems
{
    public class RenderSystem : ISystem
    {
        public List<RenderNode> Targets { get; set; }

        private readonly Screen Screen;

        public RenderSystem(Screen screen)
        {
            Screen = screen;
        }

        public void Update()
        {
            Screen.Start();
            
            foreach(var target in Targets)
            {
                Screen.Render(
                    target.Display.Sprite, 
                    target.Position.X, 
                    target.Position.Y,
                    target.RigidBody.Width,
                    target.RigidBody.Height);
            }

            Screen.Stop();
        }
    }
}
