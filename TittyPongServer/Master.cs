using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Messages;
using Lidgren.Network;
using TittyPongServer.NET;

namespace TittyPongServer
{
    public class Master
    {
        private Server MessageServer;

        
        private List<string> Clients;
        private Dictionary<string, NetConnection> ClientMap;
        
        public Master()
        {
            MessageServer = new Server();
            MessageServer.ReceivedMessageEvent += ReceivedMessageHandler;

            Clients = new List<string>();
            ClientMap = new Dictionary<string, NetConnection>();
        }

        private void ReceivedMessageHandler(object sender, ReceivedMessageEventArgs e)
        {
            var msg = e.ReceivedMessage;

            switch (msg.MessageType)
            {
                case NetIncomingMessageType.StatusChanged:
                    break;
                case NetIncomingMessageType.Data:
                    var bytes = msg.ReadBytes(msg.LengthBytes);
                    HandleDataMessage(bytes.Deserialize<Message>(), e.ReceivedMessage.SenderConnection);
                    break;
            }
        }

        private void HandleDataMessage(Message msg, NetConnection sender)
        {
            switch (msg.MessageId)
            {
                case MessageIds.ConnectionRequest:
                    var request = msg.Contents.ToString().Deserialize<ConnectionRequest>();
                    Clients.Add(request.ClientId);
                    ClientMap.Add(request.ClientId, sender);
                    // Use ToList to create a copy of the client list for thread safety?
                    // Exclude the client that sent the request from the connected clients
                    var reply = new ConnectionResponse(){AvailableClients = Clients.Where(id => id != request.ClientId).ToList()};
                    MessageServer.Send(reply.Serialize(), sender);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}