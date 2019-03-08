using Common.ECS.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ECS.Nodes;
using Common.ECS.SystemEvents;
using Common.ECS.Systems;

namespace Common.ECS
{
    public class Engine
    {
        public static int FrameNumber { get; private set; }
        private SortedList<uint, ISystem> UpdateSystems { get; set; }
        private SortedList<uint, ISystem> RenderSystems { get; set; }
        private List<Entity> Entities { get; set; }
        private ISystemContext Context;

        public Engine(ISystemContext context)
        {
            FrameNumber = 1;
            Context = context;
            UpdateSystems = new SortedList<uint, ISystem>();
            RenderSystems = new SortedList<uint, ISystem>();
            Entities = new List<Entity>();

            Context.Events.NetworkSyncEvent += OnNetworkSyncEvent;
        }

        public Engine AddSystem(ISystem system, uint priority, bool shouldRender)
        {
            var systems = (shouldRender ? RenderSystems : UpdateSystems);
            if (systems.Values.ToList().TrueForAll(s => s.GetType() != system.GetType()))
                systems.Add(priority, system);
            return this;
        }

        public Engine AddEntity(Entity entity)
        {
            Entities.Add(entity);
            Context.Events.RaiseEntityAddedEvent(entity);
            return this;
        }

        public void Update()
        {
            foreach(var system in UpdateSystems.Values)
            {
                system.Update();
            }

            FrameNumber++;
        }


        public void Render()
        {
            foreach (var system in RenderSystems.Values)
            {
                system.Update();
            }
        }

        private void OnNetworkSyncEvent(object sender, NetworkSyncEventArgs e)
        {
            if (UpdateSystems.Values.First(system => system.GetType() == typeof(SnapshotSystem)) is SnapshotSystem snapshotSystem &&
                UpdateSystems.Values.First(system => system.GetType() == typeof(MovementSystem)) is MovementSystem movementSystem &&
                UpdateSystems.Values.First(system => system.GetType() == typeof(CollisionSystem)) is CollisionSystem collisionSystem)
            {
                
                
                // get the snapshots to fast forward on
                var clientSnapshots = snapshotSystem.GetSnapshotsFromFrame(e.Snapshot.FrameNumber);
                
                if (!clientSnapshots.Any()) return; // TODO fix this bug, it's not makin snapshots
                
                var firstFrame = clientSnapshots.First();
                
                
                // check if we're off for any entities and need to sync
                var needToSync = false;
                foreach (var entity in e.Snapshot.Entities)
                {
                    var nTitty = firstFrame.SnapshotNodes.Find(node => node.NetworkIdentity.Id == entity.NetworkId);

                    if(nTitty == null) continue;
                    
                    // Check if any entity from the server is off on position by more than the divergence limit (set in the network system)
                    if (Math.Abs(nTitty.Position.X - entity.PosX) > e.DivergenceLimit || Math.Abs(nTitty.Position.Y - entity.PosY) > e.DivergenceLimit)
                    {
                        needToSync = true;
                        Debug.WriteLine("Need to sync from network!");
                        break;
                    }
                }

                if (!needToSync) return;

                // update entities
                

                // calculate  where we should be
                
            }
            else
            {
                Debug.WriteLine("Error trying to fast forward from network sync. One of the required systems could not be found");
            }
        }
    }
}
