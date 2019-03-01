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

        public static bool TryCreate(Entity target, out PlayerNode node)
        {
            node = new PlayerNode();
            if (!target.TryGetComponent(typeof(PlayerComponent), out var player)    ||
                !target.TryGetComponent(typeof(PositionComponent), out var position)||
                !target.TryGetComponent(typeof(VelocityComponent), out var velocity))
                return false;
            node.Player = player as PlayerComponent;
            node.Position = position as PositionComponent;
            node.Velocity = velocity as VelocityComponent;
            return true;
        }
    }
}
