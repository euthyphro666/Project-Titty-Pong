using System;
using Common.Game_Data;
using Microsoft.Xna.Framework;

namespace TittyPongServer
{
    public class Events
    {
        public event EventHandler<GuiLogMessageEventArgs> GuiLogMessageEvent;
        public event EventHandler<UpdateClientsEventArgs> UpdateClientsEvent;

        public void OnGuiLogMessageEvent(string msg)
        {
            GuiLogMessageEvent?.BeginInvoke(this, new GuiLogMessageEventArgs(){Message = msg}, null, null);
        }

        public void OnUpdateClientsEvent(UpdateClientsEventArgs updateClientEventArgs)
        {
            UpdateClientsEvent?.Invoke(this, updateClientEventArgs);
        }
    }

    public class GuiLogMessageEventArgs : EventArgs
    {
        public string Message { get; set; }
    }

    public class UpdateClientsEventArgs : EventArgs
    {
        public Guid RoomId { get; set; }
        public GameState ClientAState { get; set; }
        public GameState ClientBState { get; set; }
    }
}