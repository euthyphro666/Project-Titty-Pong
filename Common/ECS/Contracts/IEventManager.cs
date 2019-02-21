using Common.ECS.SystemEvents;
using System;

namespace Common.ECS.Contracts
{
    public interface IEventManager
    {
        event EventHandler<InputEventArgs> InputEvent;
        void OnInputEvent(byte input);
    }
}