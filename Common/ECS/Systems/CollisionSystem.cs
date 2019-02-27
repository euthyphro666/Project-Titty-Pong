using Common.ECS.Contracts;
using Common.ECS.Nodes;
using System.Collections.Generic;
using System.Linq;
using Common.ECS.Components;
using Common.ECS.SystemEvents;
using Common.Utils;

namespace Common.ECS.Systems
{
    public class CollisionSystem : ISystem
    {
        public uint Priority { get; set; }
        
        private readonly ISystemContext SystemContext;

        private List<CollisionNode> DynamicNodes;
        private List<CollisionNode> StaticNodes;

        private IEventManager Events;

        public CollisionSystem(ISystemContext systemContext)
        {
            SystemContext = systemContext;
            Events = SystemContext.Events;
            Events.EntityAddedEvent += HandleEntityAdded;
            DynamicNodes = new List<CollisionNode>();
            StaticNodes = new List<CollisionNode>();
        }

        private void HandleEntityAdded(object sender, EntityAddedEventArgs e)
        {
            var target = e.Target;

            if (!target.TryGetComponent(typeof(VelocityComponent), out var velocity)    ||
                !target.TryGetComponent(typeof(RigidBodyComponent), out var body)       ||
                !target.TryGetComponent(typeof(IdentityComponent), out var identity)    ||
                !target.TryGetComponent(typeof(PositionComponent), out var position))
                return;

            var node = new CollisionNode {Identity = identity as IdentityComponent, Velocity = velocity as VelocityComponent, RigidBody = body as RigidBodyComponent, Position = position as PositionComponent};

            if (node.RigidBody.IsDynamic)
                DynamicNodes.Add(node);
            else StaticNodes.Add(node);
        }


        public void Update()
        {
            CollisionNode nodeA, nodeB;

            for (var i = 0; i < DynamicNodes.Count - 1; i++)
            {
                nodeA = DynamicNodes[i];

                for (var j = i + 1; j < DynamicNodes.Count; j++)
                {
                    nodeB = DynamicNodes[j];

                    if (TittyMaths.Intersects(nodeA.RigidBody, nodeA.Position, nodeB.RigidBody, nodeB.Position))
                    {
                        //Events.RaiseCollisionEvent
                    }
                }


                // check nodeA against static nodes
                foreach (var s in StaticNodes)
                {
                    nodeB = s;

                    if (TittyMaths.Intersects(nodeA.RigidBody, nodeA.Position, nodeB.RigidBody, nodeB.Position))
                    {
                        //Events.RaiseCollisionEvent
                    }
                }
            }
        }
    }
}