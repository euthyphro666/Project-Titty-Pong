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
            Events.EntityAddedEvent += OnEntityAdded;
            DynamicNodes = new List<CollisionNode>();
            StaticNodes = new List<CollisionNode>();
        }

        private void OnEntityAdded(object sender, EntityAddedEventArgs e)
        {
            var target = e.Target;

            if (!CollisionNode.TryCreate(target, out var node)) return;
            
            if (node.RigidBody.IsDynamic)
                DynamicNodes.Add(node);
            else
                StaticNodes.Add(node);
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

                    if (Maths.Intersects(nodeA.RigidBody, nodeB.RigidBody, nodeA.Position, nodeB.Position))
                    {
                        //Events.RaiseCollisionEvent(nodeA, nodeB);
                    }
                }


                // check nodeA against static nodes
                foreach (var s in StaticNodes)
                {
                    nodeB = s;

                    if (Maths.Intersects(nodeA.RigidBody, nodeB.RigidBody, nodeA.Position, nodeB.Position))
                    {
                        //Events.RaiseCollisionEvent(nodeA, nodeB);
                    }
                }
            }
        }
    }
}