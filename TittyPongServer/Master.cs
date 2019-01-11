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
        private readonly Events Events;
        private readonly Server MessageServer;

        private readonly List<string> ClientDisplayNames;
        private Dictionary<string, NetConnection> ClientMacAddressToConnectionDictionary;
        private Dictionary<string, string> ClientMacToDisplayNameDictionary;
        
        public Master(Events events)
        {
            Events = events;
            MessageServer = new Server(events);
            MessageServer.ReceivedMessageEvent += ReceivedMessageHandler;

            ClientMacAddressToConnectionDictionary = new Dictionary<string, NetConnection>();
            ClientMacToDisplayNameDictionary = new Dictionary<string, string>();
        }

        private void ReceivedMessageHandler(object sender, ReceivedMessageEventArgs e)
        {
            var msg = e.ReceivedMessage;

            switch (msg.MessageType)
            {
                case NetIncomingMessageType.StatusChanged:
                    
                    Events.OnGuiLogMessageEvent($"New status message: {e.ReceivedMessage.MessageType}:{(NetConnectionStatus)e.ReceivedMessage.ReadByte()} from {e.ReceivedMessage.SenderConnection}");
                    break;
                case NetIncomingMessageType.Data:
                    var bytes = msg.ReadBytes(msg.LengthBytes);
                    HandleDataMessage(bytes.Deserialize<Message>(), e.ReceivedMessage.SenderConnection);
                    break;
                case NetIncomingMessageType.ConnectionApproval:
                    e.ReceivedMessage.SenderConnection.Approve();
                    break;
            }
        }

        private void HandleDataMessage(Message msg, NetConnection sender)
        {
            if(msg.Contents == null)
            {
                Events.OnGuiLogMessageEvent($"Received data message '{msg.MessageId}' with null contents");
                return;
            }
            
            switch (msg.MessageId)
            {
                case MessageIds.ConnectionRequest:
                    var connectionRequest = msg.Contents.ToString().Deserialize<ConnectionRequest>();
                    ClientMacAddressToConnectionDictionary[connectionRequest.ClientId] = sender;
                    ClientMacToDisplayNameDictionary[connectionRequest.ClientId] = connectionRequest.DisplayName;
                    // Use ToList to create a copy of the client list for thread safety?
                    // Exclude the client that sent the request from the connected clients
                    var reply = new ConnectionResponse(){AvailableClients = ClientMacToDisplayNameDictionary};
                    var responseMessage = new Message(){MessageId = ConnectionResponse.MessageId, Contents = reply};
                    
                    // Broadcast that a client connected
                    MessageServer.Broadcast(responseMessage.Serialize());
                    Events.OnGuiLogMessageEvent($"New client connected: {connectionRequest.ClientId}");
                    break;
                case MessageIds.StartGameRequest:
                    HandleStartGameRequest(msg);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleStartGameRequest(Message msg)
        {
            var request = msg.Contents.ToString().Deserialize<StartGameRequest>();
            var targetClient = GetConnectionFromMac(request.TargetClientMac);
            if (targetClient == null) return; // Or return failed to request game message  // TODO

            var forwardedMessage = new Message(){MessageId = StartGameRequest.MessageId, Contents = request};
            MessageServer.Send(forwardedMessage.Serialize(), targetClient, NetDeliveryMethod.ReliableOrdered);
        }

        private NetConnection GetConnectionFromMac(string mac)
        {
            ClientMacAddressToConnectionDictionary.TryGetValue(mac, out var connection);
            return connection;
        }
    }
}