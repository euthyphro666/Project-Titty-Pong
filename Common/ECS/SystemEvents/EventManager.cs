using Common.ECS.Components;
using Common.ECS.Contracts;
using Common.ECS.Nodes;
using Common.IO;
using System;
using Common.Networking;
using System.Collections.Generic;

namespace Common.ECS.SystemEvents
{
    public class EventManager : IEventManager
    {

        public event EventHandler<SnapshotEventArgs> SnapshotEvent;
        public void RaiseSnapshotEvent(List<DynamicSnapshotNode> snapshot)
        {
            SnapshotEvent?.Invoke(null, new SnapshotEventArgs { Snapshot = snapshot });
        }

        public event EventHandler<CollisionEventArgs> CollisionEvent;
        public void RaiseCollisionEvent(CollisionNode node1, CollisionNode node2)
        {
            CollisionEvent?.Invoke(null, new CollisionEventArgs { Node1 = node1, Node2 = node2 });
        }

        public event EventHandler<InputEventArgs> InputEvent;
        public void RaiseInputEvent(PlayerNumber player, Input input, int frameNumber)
        {
            InputEvent?.Invoke(null, new InputEventArgs { Player = player, Input = input, Frame = frameNumber});
        }

        public event EventHandler<EntityAddedEventArgs> EntityAddedEvent;
        public void RaiseEntityAddedEvent(Entity target)
        {
            EntityAddedEvent?.Invoke(null, new EntityAddedEventArgs { Target = target });
        }

        public event EventHandler<GameSnapshotEventArgs> GameSnapshotEvent;
        public void RaiseGameSnapshotEvent(GameSnapshot gs)
        {
            GameSnapshotEvent?.Invoke(null, new GameSnapshotEventArgs(gs));
        }

        public event EventHandler<NetworkSyncEventArgs> NetworkSyncEvent;
        public void RaiseNetworkSyncEvent(NetworkSnapshot state, float divergenceLimit)
        {
            NetworkSyncEvent?.Invoke(null, new NetworkSyncEventArgs(state, divergenceLimit));
        }
    }
}