using System;
using System.Collections.Generic;

namespace TittyPong.Events
{
    public class ClientListReceivedEventArgs : EventArgs
    {
        public Dictionary<string, string> Clients { get; set; }

        public ClientListReceivedEventArgs(Dictionary<string, string> clients)
        {
            Clients = clients;
        }
    }
}