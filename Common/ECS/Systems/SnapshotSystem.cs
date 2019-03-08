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
        private List<GameSnapshot> Snapshots;
        private Input LastInput;
        
        public SnapshotSystem(ISystemContext systemContext)
        {
            Snapshots = new List<GameSnapshot>(256);
            SystemContext = systemContext;
            SystemContext.Events.EntityAddedEvent += OnEntityAddedEvent;
            SystemContext.Events.InputEvent += OnInputEvent;
            
            Nodes = new List<DynamicSnapshotNode>();
        }

        public void Update()
        {
            // Create a new GameSnapshot and queue it up
            var snapshot = new GameSnapshot(Engine.FrameNumber, Nodes.ToList(), LastInput);
            SystemContext.Events.RaiseGameSnapshotEvent(snapshot);
            
            
            if (Snapshots.Count > 255)
            {
                Snapshots.RemoveAt(0);
            }
            Snapshots.Add(snapshot);

        }

        private void OnInputEvent(object sender, InputEventArgs e)
        {
            LastInput = e.Input;
        }

        private void OnEntityAddedEvent(object sender, EntityAddedEventArgs args)
        {
            
            var target = args.Target;
            if (!DynamicSnapshotNode.TryCreate(target, out var node)) return;
            Nodes.Add(node);
        }

        public List<GameSnapshot> GetSnapshotsFromFrame(int frameNumber)
        {
            return Snapshots.Where(snapshot => snapshot.FrameNumber >= frameNumber).ToList();
        }
    }
}