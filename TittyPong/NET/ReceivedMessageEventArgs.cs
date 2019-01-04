using System;
using Lidgren.Network;

namespace TittyPong.NET
{
    public class ReceivedMessageEventArgs : EventArgs
    {
        public NetIncomingMessage ReceivedMessage { get; set; }
    }
}