using Common.ECS.Contracts;
using Common.ECS.Nodes;
using System.Collections.Generic;
using Common.ECS.Components;
using Common.Utils;

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
            CollisionNode nodeA, nodeB;

            for (var i = 0; i < Nodes.Count - 1; i++)
            {
                nodeA = Nodes[i];

                for (var j = i + 1; j < Nodes.Count; j++)
                {
                    nodeB = Nodes[j];

                    if (TittyMaths.Intersects(nodeA.RigidBody, nodeA.Position, nodeB.RigidBody, nodeB.Position))
                    {
                        //Events.RaiseCollisionEvent
                    }
                }
            }
        }

        
        
    }
}