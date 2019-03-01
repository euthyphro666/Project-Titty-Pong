using Common.ECS.Components;
using Common.ECS.SystemEvents;
using Common.IO;
using System;

namespace Common.ECS.Contracts
{
    public interface IEventManager
    {
        event EventHandler<InputEventArgs> InputEvent;
        void RaiseInputEvent(PlayerNumber player, Input input);

        event EventHandler<EntityAddedEventArgs> EntityAddedEvent;
        void RaiseEntityAddedEvent(Entity e);

        event EventHandler<GameSnapshotEventArgs> GameSnapshotEvent;
        void RaiseGameSnapshotEvent(GameSnapshot gs);
    }
}