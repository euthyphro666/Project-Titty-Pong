using System;
using Common.ECS.Components;
using Common.ECS.Contracts;

namespace Common.ECS.Nodes
{
    public class CollisionNode : INode
    {
        public VelocityComponent Velocity { get; set; }
        public PositionComponent Position { get; set; }
        public RigidBodyComponent RigidBody { get; set; }
        public IdentityComponent Identity { get; set; }

        public static bool TryCreate(Entity entity, out CollisionNode node)
        {
            if (!entity.TryGetComponent(typeof(VelocityComponent), out var velocity) ||
                !entity.TryGetComponent(typeof(RigidBodyComponent), out var body) ||
                !entity.TryGetComponent(typeof(IdentityComponent), out var identity) ||
                !entity.TryGetComponent(typeof(PositionComponent), out var position))
            {
                node = null;
                return false;
            }

            node = new CollisionNode()
            {
                Velocity = velocity as VelocityComponent,
                Identity = identity as IdentityComponent,
                Position = position as PositionComponent,
                RigidBody = body as RigidBodyComponent
            };
            return true;
        }
    }
}