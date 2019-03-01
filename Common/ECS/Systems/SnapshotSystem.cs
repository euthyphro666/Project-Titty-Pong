using System.Collections.Generic;
using System.Linq;
using Common.ECS.Components;
using Common.ECS.Contracts;
using Common.ECS.Nodes;
using Common.ECS.SystemEvents;
using Common.IO;

namespace Common.ECS.Systems
{
    public class SnapshotSystem : ISystem
    {
        private readonly ISystemContext SystemContext;
        public uint Priority { get; set; }

        private List<DynamicSnapshotNode> Nodes;
        private Input LastInput;

        public SnapshotSystem(ISystemContext systemContext)
        {
            SystemContext = systemContext;
            SystemContext.Events.EntityAddedEvent += OnEntityAddedEvent;
            SystemContext.Events.InputEvent += OnInputEvent;
            
            Nodes = new List<DynamicSnapshotNode>();
        }

        public void Update()
        {
            // Create a new GameSnapshot and queue it up
            var snapshot = new GameSnapshot(Nodes.ToList(), LastInput);
            SystemContext.Events.RaiseGameSnapshotEvent(snapshot);
        }

        private void OnInputEvent(object sender, InputEventArgs e)
        {
            LastInput = e.Input;
        }

        private void OnEntityAddedEvent(object sender, EntityAddedEventArgs args)
        {
            
            var target = args.Target;
            if (DynamicSnapshotNode.TryCreate(target, out var node))
            {
                Nodes.Add(node);
            }
        }
    }
}