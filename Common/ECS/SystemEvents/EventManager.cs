using Common.ECS.Components;
using Common.ECS.Contracts;
using Common.ECS.Nodes;
using Common.IO;
using System;

namespace Common.ECS.SystemEvents
{
    public class EventManager : IEventManager
    {

        public event EventHandler<CollisionEventArgs> CollisionEvent;
        public void RaiseCollisionEvent(CollisionNode node1, CollisionNode node2)
        {
            CollisionEvent?.Invoke(null, new CollisionEventArgs { Node1 = node1, Node2 = node2 });
        }

        public event EventHandler<InputEventArgs> InputEvent;
        public void RaiseInputEvent(PlayerNumber player, Input input)
        {
            InputEvent?.Invoke(null, new InputEventArgs { Player = player, Input = input });
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
    }
}