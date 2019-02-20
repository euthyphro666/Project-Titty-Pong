using Common.ECS.Components;

namespace Common.ECS.Nodes
{
    public class CollisionNode
    {
        public VelocityComponent Velocity { get; set; }
        public PositionComponent Position { get; set; }
        public IdentityComponent IdentityA { get; set; }
        public IdentityComponent IdentityB { get; set; }
    }
}