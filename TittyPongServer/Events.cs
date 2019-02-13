using System;
using Common.Game_Data;
using Microsoft.Xna.Framework;

namespace TittyPongServer
{
    public class Events
    {
        public event EventHandler<GuiLogMessageEventArgs> GuiLogMessageEvent;
        public event EventHandler<UpdateClientsEventArgs> UpdateClientsEvent;
        public event EventHandler<StartGameEventArgs> StartGameEvent;

        public void OnGuiLogMessageEvent(string msg)
        {
            GuiLogMessageEvent?.BeginInvoke(this, new GuiLogMessageEventArgs(){Message = msg}, null, null);
        }
        
        public void OnStartGameEvent(Guid roomId, string clientAId, string clientBId, long currentTick)
        {
            StartGameEvent?.Invoke(this, new StartGameEventArgs(){RoomId = roomId, ClientAId = clientAId, ClientBId = clientBId, NetworkSyncTime = currentTick});
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
        public long NetworkTimeSync { get; set; }
    }
    
    public class StartGameEventArgs : EventArgs
    {
        public Guid RoomId { get; set; }
        /// <summary>
        /// The current server tick
        /// </summary>
        public long NetworkSyncTime { get; set; } 
        public string ClientAId { get; set; }
        public string ClientBId { get; set; }
    }
}