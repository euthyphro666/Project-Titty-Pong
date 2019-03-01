using System;
using Common.ECS.Components;
using Common.ECS.Contracts;

namespace Common.ECS.Nodes
{
    public class MovementNode : INode
    {
        public PositionComponent Position { get; set; }
        public VelocityComponent Velocity { get; set; }

        public static bool TryCreate(Entity target, out MovementNode node)
        {
            node = new MovementNode();
            if (!target.TryGetComponent(typeof(PositionComponent), out var position) ||
                !target.TryGetComponent(typeof(VelocityComponent), out var velocity))
                return false;
            node.Position = position as PositionComponent;
            node.Velocity = velocity as VelocityComponent;
            return true;
        }
    }
}