using System;

namespace TittyPongServer
{
    public class Events
    {
        public event EventHandler<GuiLogMessageEventArgs> GuiLogMessageEvent;

        public void RaiseGuiLogMessageEvent(string msg)
        {
            GuiLogMessageEvent?.Invoke(this, new GuiLogMessageEventArgs(){Message = msg});
        }
    }

    public class GuiLogMessageEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}