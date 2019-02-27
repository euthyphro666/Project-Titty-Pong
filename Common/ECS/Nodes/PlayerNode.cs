using Common.ECS.Components;
using Common.ECS.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ECS.Nodes
{
    public class PlayerNode : INode
    {
        public PlayerComponent Player { get; set; }
        public PositionComponent Position { get; set; }
        public VelocityComponent Velocity { get; set; }

        public static bool Uses(Type component)
        {
            return (component == typeof(PlayerComponent) || 
                    component == typeof(PositionComponent) ||
                    component == typeof(VelocityComponent));
        }
    }
}
