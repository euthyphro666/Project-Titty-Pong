using System;
using Lidgren.Network;

namespace TittyPongServer.NET
{
    public class Server
    {
        private NetServer LidgrenServer;

        public event EventHandler<ReceivedMessageEventArgs> ReceivedMessageEvent;
        
        
        public Server()
        {
            var config = new NetPeerConfiguration("TittyPong");
            LidgrenServer = new NetServer(config);
            LidgrenServer.Start();
            
            LidgrenServer.RegisterReceivedCallback(Callback);
        }

        private void Callback(object state)
        {
            throw new System.NotImplementedException();
        }
    }
}