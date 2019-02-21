using System;
using Common.ECS.Components;
using Common.ECS.Contracts;

namespace Common.ECS.Nodes
{
    public class NetworkSyncNode : INode
    {
        public PositionComponent Position { get; set; }
        public VelocityComponent Velocity { get; set; }
        
        
        public static bool Uses(Type component)
        {
            return (component == typeof(PositionComponent) || component == typeof(VelocityComponent));
        }
    }
}