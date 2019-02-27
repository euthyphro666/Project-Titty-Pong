using System;
using Common.ECS.Components;
using Common.ECS.Contracts;

namespace Common.ECS.Nodes
{
    public class SnapshotNode : INode
    {
        // TODO change to some kind of input thing
        public DisplayComponent Display { get; set; }
        
        public static bool Uses(Type component)
        {
            return (component == typeof(PositionComponent));
        }
    }
}