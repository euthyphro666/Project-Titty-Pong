using System;
using Lidgren.Network;
using TittyPongServer.NET;

namespace TittyPongServer
{
    public class Master
    {

        private Server MessageServer;

        public Master()
        {

            MessageServer = new Server();
            MessageServer.ReceivedMessageEvent += ReceivedMessageHandler;
        }

        private void ReceivedMessageHandler(object sender, ReceivedMessageEventArgs e)
        {
            var msg = e.ReceivedMessage;

            switch (msg.MessageType)
            {
                case NetIncomingMessageType.StatusChanged:
                    break;
                case NetIncomingMessageType.Data:
                    break;
            }
        }
    }
}