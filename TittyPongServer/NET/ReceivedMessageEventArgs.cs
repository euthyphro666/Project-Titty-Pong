using System;
using Lidgren.Network;

namespace TittyPongServer.NET
{
    public class ReceivedMessageEventArgs : EventArgs
    {
        public NetIncomingMessage ReceivedMessage { get; set; }
    }
}