using Common.ECS.Contracts;
using System;

namespace Common.ECS.SystemEvents
{
    public class EventManager : IEventManager
    {
        public event EventHandler<InputEventArgs> InputEvent;
        public void RaiseInputEvent(byte input)
        {
            InputEvent?.Invoke(null, new InputEventArgs { Input = input });
        }
    }
}