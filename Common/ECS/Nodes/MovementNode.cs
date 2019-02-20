using Common.ECS.Components;

namespace Common.ECS.Nodes
{
    public class MovementNode
    {
        public PositionComponent Position { get; set; }
        public VelocityComponent Velocity { get; set; }
    }
}