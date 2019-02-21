using Common.ECS.Contracts;
using Common.ECS.Nodes;
using System.Collections.Generic;

namespace Common.ECS.Systems
{
    public class CollisionSystem : ISystem
    {

        private List<CollisionNode> Nodes;

        private IEventManager Events;
        
        public CollisionSystem(IEventManager eventManager)
        {
            Events = eventManager;
        }
        
        public void Update()
        {

        }
    }
}
