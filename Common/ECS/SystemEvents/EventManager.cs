using Common.ECS.Components;
using Common.ECS.Contracts;
using Common.IO;
using System;

namespace Common.ECS.SystemEvents
{
    public class EventManager : IEventManager
    {
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
    }
}