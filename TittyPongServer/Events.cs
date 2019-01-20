using System;

namespace TittyPongServer
{
    public class Events
    {
        public event EventHandler<GuiLogMessageEventArgs> GuiLogMessageEvent;
        public event EventHandler<UpdateClientsEventArgs> UpdateClientsEvent;

        public void OnGuiLogMessageEvent(string msg)
        {
            GuiLogMessageEvent?.Invoke(this, new GuiLogMessageEventArgs(){Message = msg});
        }

        public void OnUpdateClientsEvent(UpdateClientsEventArgs updateClientEventArgs)
        {
            throw new NotImplementedException();
        }
    }

    public class GuiLogMessageEventArgs : EventArgs
    {
        public string Message { get; set; }
    }

    public class UpdateClientsEventArgs : EventArgs
    {
        public string ClientAId { get; set; }
        public string ClientBId { get; set; }
        public object GameData { get; set; }
    }
}