using System;

namespace TittyPong.IO
{
    public interface IMouseEventListeners
    {
        event EventHandler<InputMouseEventArgs> MouseDownEvent;
        event EventHandler<InputMouseEventArgs> MouseUpEvent;
        event EventHandler<InputMouseEventArgs> MouseHeldEvent;

        event EventHandler<InputMouseEventArgs> MouseHoverEvent;
    }
}