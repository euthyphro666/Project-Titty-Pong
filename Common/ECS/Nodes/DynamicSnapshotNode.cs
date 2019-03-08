using System;
using Common.ECS.Components;
using Common.ECS.Contracts;

namespace Common.ECS.Nodes
{
    public class DynamicSnapshotNode : INode
    {
        public PositionComponent Position { get; set; }
        public VelocityComponent Velocity { get; set; }
        public NetworkIdentityComponent NetworkIdentity { get; set; }
        
        public static bool TryCreate(Entity entity, out DynamicSnapshotNode node)
        { 
            if (!entity.TryGetComponent(typeof(VelocityComponent), out var velocity) ||
              !entity.TryGetComponent(typeof(NetworkIdentityComponent), out var identity) ||
              !entity.TryGetComponent(typeof(PositionComponent), out var position))
            {
                node = null;
                return false;
            }

            node = new DynamicSnapshotNode()
            {
                Velocity = velocity as VelocityComponent,
                NetworkIdentity = identity as NetworkIdentityComponent,
                Position = position as PositionComponent,
            };
            return true;
        }
    }
}