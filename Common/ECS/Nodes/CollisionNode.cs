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

        public static bool Uses(IComponent component)
        {
            return (component is VelocityComponent || component is PositionComponent ||
                    component is RigidBodyComponent || component is IdentityComponent);
        }
    }
}