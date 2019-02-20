using Common.ECS.Components;

namespace Common.ECS.Nodes
{
    public class CollisionNode
    {
        public VelocityComponent Velocity { get; set; }
        public PositionComponent Position { get; set; }
        public RigidBodyComponent RigidBody { get; set; }
        public IdentityComponent Identity { get; set; }
    }
}