using System;
using System.Collections.Generic;

namespace TittyPong.Events
{
    public class ClientListReceivedEventArgs : EventArgs
    {
        public List<string> Clients { get; set; }

        public ClientListReceivedEventArgs(List<string> clients)
        {
            Clients = clients;
        }
    }
}