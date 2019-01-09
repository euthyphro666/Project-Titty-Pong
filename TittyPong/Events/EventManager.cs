using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TittyPong.IO;

namespace TittyPong.Events
{
    public class EventManager
    {

        #region Input Events
        public event EventHandler<InputCommandEventArgs> CommandEvent;
        public void OnCommandEvent(object sender, InputCommandEventArgs e)
        {
            CommandEvent?.Invoke(sender, e);
        }

        public event EventHandler<InputDirectionEventArgs> DirectionEvent;
        public void OnDirectionEvent(object sender, InputDirectionEventArgs e)
        {
            DirectionEvent?.Invoke(sender, e);
        }

        public event EventHandler<InputButtonEventArgs> ButtonDownEvent;
        public void OnButtonDownEvent(object sender, InputButtonEventArgs e)
        {
            ButtonDownEvent?.Invoke(sender, e);
        }

        public event EventHandler<InputButtonEventArgs> ButtonHeldEvent;
        public void OnButtonHeldEvent(object sender, InputButtonEventArgs e)
        {
            ButtonHeldEvent?.Invoke(sender, e);
        }

        public event EventHandler<InputButtonEventArgs> ButtonUpEvent;
        public void OnButtonUpEvent(object sender, InputButtonEventArgs e)
        {
            ButtonUpEvent?.Invoke(sender, e);
        }

        public event EventHandler<InputMouseEventArgs> MouseDownEvent;
        public void OnMouseDownEvent(object sender, InputMouseEventArgs e)
        {
            MouseDownEvent?.Invoke(sender, e);
        }

        public event EventHandler<InputMouseEventArgs> MouseUpEvent;
        public void OnMouseUpEvent(object sender, InputMouseEventArgs e)
        {
            MouseUpEvent?.Invoke(sender, e);
        }

        public event EventHandler<InputMouseEventArgs> MouseHeldEvent;
        public void OnMouseHeldEvent(object sender, InputMouseEventArgs e)
        {
            MouseHeldEvent?.Invoke(sender, e);
        }

        public event EventHandler<InputMouseEventArgs> MouseHoverEvent;
        public void OnMouseHoverEvent(object sender, InputMouseEventArgs e)
        {
            MouseHoverEvent?.Invoke(sender, e);
        }
        #endregion

        public event EventHandler<StringEventArgs> ConnectionInfoEvent;
        public void OnConnectionInfoEvent(object sender, StringEventArgs e)
        {
            ConnectionInfoEvent?.Invoke(sender, e);
        }

        public event EventHandler<ByteArrayEventArgs> SendMessageEvent;
        public void OnSendMessageEvent(object sender, ByteArrayEventArgs e)
        {
            SendMessageEvent?.Invoke(sender, e);
        }

    }
}
