using Common.ECS.Contracts;
using Common.ECS.Nodes;
using System.Collections.Generic;
using Common.ECS.Components;

namespace Common.ECS.Systems
{
    public class CollisionSystem : ISystem
    {
        private readonly ISystemContext SystemContext;

        private List<CollisionNode> Nodes;

        private IEventManager Events;

        public CollisionSystem(ISystemContext systemContext)
        {
            SystemContext = systemContext;
            Events = SystemContext.Events;
            Nodes = new List<CollisionNode>();
        }

        public void Update()
        {
            foreach (var nodeA in Nodes)
            {
                foreach (var nodeB in Nodes)
                {
                    if (nodeA == nodeB) continue;

                    if (Intersects(nodeA.RigidBody, nodeA.Position, nodeA.RigidBody, nodeA.Position))
                    {
                        //Events.RaiseCollisionEvent
                    }
                    
                }
            }
        }

        private bool Intersects(RigidBodyComponent bodyA, PositionComponent posA, RigidBodyComponent bodyB, PositionComponent posB)
        {
            if (bodyA.IsRect)
            {
                if (bodyB.IsRect)
                {
                    
                }
                else
                {
                    
                }
            }
            else
            {
                if (bodyB.IsRect)
                {
                    
                }
                else
                {
                    
                }
            }
            return false;
        }
}
}
