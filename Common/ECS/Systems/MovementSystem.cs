using Common.ECS.Contracts;
using Common.ECS.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ECS.Contracts;

namespace Common.ECS.Systems
{
    public class MovementSystem : ISystem
    {
        public uint Priority { get; set; }

        private readonly ISystemContext SystemContext;
        private List<MovementNode> Nodes;

        private IEventManager Events;

        public MovementSystem(ISystemContext systemContext)
        {
            SystemContext = systemContext;
            Events = SystemContext.Events;
            Nodes = new List<MovementNode>();
        }
        
        public void Update()
        {

        }
    }
}