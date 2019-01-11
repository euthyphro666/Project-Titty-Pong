using System;
using System.Collections.Generic;

namespace TittyPong.Events
{
    public class ClientListReceivedEventArgs : EventArgs
    {
        public Dictionary<string, string> ClientMacToDisplayDictionary { get; set; }

        public ClientListReceivedEventArgs(Dictionary<string, string> clients)
        {
            ClientMacToDisplayDictionary = clients;
        }
    }
}