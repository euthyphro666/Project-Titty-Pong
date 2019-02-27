using System.Collections.Generic;
using Common.ECS.Components;
using Common.ECS.Contracts;
using Common.ECS.Nodes;
using Common.ECS.SystemEvents;

namespace Common.ECS.Systems
{
    public class SnapshotSystem : ISystem
    {
        private readonly ISystemContext SystemContext;

        private List<SnapshotNode> Nodes;
        
        public SnapshotSystem(ISystemContext systemContext)
        {
            SystemContext = systemContext;
            SystemContext.Events.EntityAddedEvent += OnEntityAddedEvent;
            
            Nodes = new List<SnapshotNode>();
        }

        public uint Priority { get; set; }

        public void Update()
        {
            
        }

        private void OnEntityAddedEvent(object sender, EntityAddedEventArgs args)
        {
            // TODO change this to some kind of input register
            
            var target = args.Target;
            if( target.TryGetComponent(typeof(PositionComponent), out var pos))
            {
                Nodes.Add(new SnapshotNode()
                {
                    //Position = pos as PositionComponent
                });
            }
        }
    }
}