using System;
using Common.ECS.Components;
using Common.ECS.Contracts;

namespace Common.ECS.Nodes
{
    [Serializable]
    public class DynamicSnapshotNode : INode
    {
        public PositionComponent Position { get; set; }
        public VelocityComponent Velocity { get; set; }
        public NetworkIdentityComponent NetworkIdentity { get; set; }
        
        public static bool TryCreate(Entity entity, out DynamicSnapshotNode node)
        {
throw new NotImplementedException();
            node = null;
            return false;
        }
    }
}