using Common.ECS.Components;
using Common.ECS.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ECS.Nodes
{
    public class LerpableRenderNode : INode
    {
        public DisplayComponent Display { get; set; }
        public PositionComponent Position { get; set; }
        public RigidBodyComponent RigidBody { get; set; }
        public NetworkIdentityComponent NetId { get; set; }

        public static bool TryCreate(Entity target, out LerpableRenderNode node)
        {
            node = new LerpableRenderNode();
            if (!target.TryGetComponent(typeof(DisplayComponent), out var display) ||
                !target.TryGetComponent(typeof(RigidBodyComponent), out var body) ||
                !target.TryGetComponent(typeof(PositionComponent), out var position) ||
                !target.TryGetComponent(typeof(NetworkIdentityComponent), out var netId))
                return false;
            node.Display = display as DisplayComponent;
            node.RigidBody = body as RigidBodyComponent;
            node.Position = position as PositionComponent;
            node.NetId = netId as NetworkIdentityComponent;
            return true;
        }
    }
}
