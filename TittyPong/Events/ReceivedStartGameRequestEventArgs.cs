using System;

namespace TittyPong.Events
{
    public class ReceivedStartGameRequestEventArgs : EventArgs
    {
        public string RequestingClientDisplayName { get; set; }
        public string RequestingClientMac { get; set; }
    }
}