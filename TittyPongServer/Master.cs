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

        
        private readonly List<string> ClientMacAddresses;
        private readonly List<string> ClientDisplayNames;
        private Dictionary<string, NetConnection> ClientMacAddressMap;
        private Dictionary<string, string> ClientDisplayNameMap;
        
        public Master(Events events)
        {
            Events = events;
            MessageServer = new Server(events);
            MessageServer.ReceivedMessageEvent += ReceivedMessageHandler;

            ClientMacAddresses = new List<string>();
            ClientMacAddressMap = new Dictionary<string, NetConnection>();
            ClientDisplayNameMap = new Dictionary<string, string>();
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
                    var request = msg.Contents.ToString().Deserialize<ConnectionRequest>();
                    ClientMacAddresses.Add(request.ClientId);
                    ClientMacAddressMap.Add(request.ClientId, sender);
                    ClientDisplayNameMap.Add(request.ClientId, request.DisplayName);
                    // Use ToList to create a copy of the client list for thread safety?
                    // Exclude the client that sent the request from the connected clients
                    var reply = new ConnectionResponse(){AvailableClients = ClientDisplayNameMap};
                    var responseMessage = new Message(){MessageId = ConnectionResponse.MessageId, Contents = reply};
                    
                    // Broadcast that a client connected
                    MessageServer.Broadcast(responseMessage.Serialize());
                    Events.OnGuiLogMessageEvent($"New client connected: {request.ClientId}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}