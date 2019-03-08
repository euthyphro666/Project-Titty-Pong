using Common.ECS.Components;
using Common.ECS.Nodes;
using Common.ECS.SystemEvents;
using Common.IO;
using System;
using Common.Networking;
using System.Collections.Generic;

namespace Common.ECS.Contracts
{
    public interface IEventManager
    {
        event EventHandler<SnapshotEventArgs> SnapshotEvent;
        void RaiseSnapshotEvent(List<DynamicSnapshotNode> snapshot);

        event EventHandler<CollisionEventArgs> CollisionEvent;
        void RaiseCollisionEvent(CollisionNode node1, CollisionNode node2);

        event EventHandler<InputEventArgs> InputEvent;
        void RaiseInputEvent(PlayerNumber player, Input input, int frameNumber);

        event EventHandler<EntityAddedEventArgs> EntityAddedEvent;
        void RaiseEntityAddedEvent(Entity e);

        event EventHandler<GameSnapshotEventArgs> GameSnapshotEvent;
        void RaiseGameSnapshotEvent(GameSnapshot gs);
        
        event EventHandler<NetworkSyncEventArgs> NetworkSyncEvent;
        void RaiseNetworkSyncEvent(NetworkSnapshot state, float divergenceLimit);
    }
}