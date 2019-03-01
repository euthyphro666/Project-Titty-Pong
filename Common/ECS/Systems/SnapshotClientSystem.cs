using System.Collections.Generic;
using System.Linq;
using Common.ECS.Components;
using Common.ECS.Contracts;
using Common.ECS.Nodes;
using Common.ECS.SystemEvents;

namespace Common.ECS.Systems
{
    public class SnapshotClientSystem : ISystem
    {
        private readonly ISystemContext SystemContext;
        public uint Priority { get; set; }

        private List<DynamicSnapshotNode> Nodes;
        private Queue<GameSnapshot> Snapshots;
        private Input LastInput;

        public SnapshotClientSystem(ISystemContext systemContext)
        {
            SystemContext = systemContext;
            SystemContext.Events.EntityAddedEvent += OnEntityAddedEvent;
            SystemContext.Events.InputEvent += OnInputEvent;
            
            Nodes = new List<DynamicSnapshotNode>();
            Snapshots = new Queue<GameSnapshot>();
        }

        public void Update()
        {
            // Create a new GameSnapshot and queue it up
            Snapshots.Enqueue(new GameSnapshot(Nodes.ToList(), LastInput));
        }

        private void OnInputEvent(object sender, InputEventArgs e)
        {
            LastInput = e.Input;
        }

        private void OnEntityAddedEvent(object sender, EntityAddedEventArgs args)
        {
            // TODO change this to some kind of input register
            
            var target = args.Target;
            if( target.TryGetComponent(typeof(PositionComponent), out var pos))
            {
                Nodes.Add(new DynamicSnapshotNode()
                {
                    //Position = pos as PositionComponent
                    
                });
            }
        }
    }
}