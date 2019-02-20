using Common.ECS.Components;
using Common.ECS.Contracts;

namespace Common.ECS.Nodes
{
    public class MovementNode : INode
    {
        public PositionComponent Position { get; set; }
        public VelocityComponent Velocity { get; set; }

        public static bool Uses(IComponent component)
        {
            return (component is PositionComponent || component is VelocityComponent);
        }
    }
}