using Common.ECS.Components;
using Common.ECS.Contracts;
using Common.ECS.Nodes;
using Common.ECS.SystemEvents;
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
        public uint Priority { get; set; }
        private readonly ISystemContext SystemContext;
        public List<RenderNode> Targets { get; set; }

        private readonly Screen Screen;
        private IEventManager Events;
        
        public RenderSystem(ISystemContext systemContext, Screen screen)
        {
            SystemContext = systemContext;
            Events = SystemContext.Events;
            Targets = new List<RenderNode>();
            Screen = screen;

            Events.EntityAddedEvent += OnEntityAddedEvent;
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


        public void OnEntityAddedEvent(object sender, EntityAddedEventArgs args)
        {
            var target = args.Target;
            if( target.TryGetComponent(typeof(DisplayComponent), out var display) &&
                target.TryGetComponent(typeof(RigidBodyComponent), out var body) &&
                target.TryGetComponent(typeof(PositionComponent), out var position))
            {
                Targets.Add(new RenderNode
                {
                    Display = display as DisplayComponent,
                    RigidBody = body as RigidBodyComponent,
                    Position = position as PositionComponent
                });
            }
        }

    }
}
