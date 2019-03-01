using Common.ECS.Components;
using Common.ECS.Nodes;
using Common.ECS.SystemEvents;
using Common.IO;
using System;

namespace Common.ECS.Contracts
{
    public interface IEventManager
    {
        event EventHandler<CollisionEventArgs> CollisionEvent;
        void RaiseCollisionEvent(CollisionNode node1, CollisionNode node2);

        event EventHandler<InputEventArgs> InputEvent;
        void RaiseInputEvent(PlayerNumber player, Input input);

        event EventHandler<EntityAddedEventArgs> EntityAddedEvent;
        void RaiseEntityAddedEvent(Entity e);

        event EventHandler<GameSnapshotEventArgs> GameSnapshotEvent;
        void RaiseGameSnapshotEvent(GameSnapshot gs);
    }
}