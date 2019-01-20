using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Messages;
using Lidgren.Network;
using TittyPongServer.Game_Room;
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

        private Dictionary<Guid, Room> OpenRooms;
        
        public Master(Events events)
        {
            Events = events;
            MessageServer = new Server(events);
            MessageServer.ReceivedMessageEvent += ReceivedMessageHandler;

            ClientMacAddressToConnectionDictionary = new Dictionary<string, NetConnection>();
            ClientMacToDisplayNameDictionary = new Dictionary<string, string>();
            OpenRooms = new Dictionary<Guid, Room>();
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
                Events.OnGuiLogMessageEvent($"Received data message '{msg.CommunicationMessageId}' with null contents");
                return;
            }
            
            switch (msg.CommunicationMessageId)
            {
                case CommunicationMessageIds.ConnectionRequest:
                    var connectionRequest = msg.Contents.ToString().Deserialize<ConnectionRequest>();
                    ClientMacAddressToConnectionDictionary[connectionRequest.ClientId] = sender;
                    ClientMacToDisplayNameDictionary[connectionRequest.ClientId] = connectionRequest.DisplayName;
                    // Use ToList to create a copy of the client list for thread safety?
                    // Exclude the client that sent the request from the connected clients
                    var reply = new ConnectionResponse(){AvailableClients = ClientMacToDisplayNameDictionary};
                    var responseMessage = new Message(){CommunicationMessageId = ConnectionResponse.MessageId, Contents = reply};
                    
                    // Broadcast that a client connected
                    MessageServer.Broadcast(responseMessage.Serialize());
                    Events.OnGuiLogMessageEvent($"New client connected: {connectionRequest.ClientId}");
                    break;
                case CommunicationMessageIds.StartGameRequest:
                    HandleStartGameRequest(msg);
                    break;
                case CommunicationMessageIds.StartGameResponse:
                    HandleStartGameResponse(msg);
                    break;
                case CommunicationMessageIds.RoomMessage:
                    var updateMessage = msg.Contents.ToString().Deserialize<RoomMessage>();
                    var roomId = updateMessage.RoomId;
                    
                    OpenRooms[roomId].HandleMessage(updateMessage);
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Handles a start game response from a client.
        /// This is received from client B when client A sends a start game request to B.
        /// </summary>
        /// <param name="msg">The response message</param>
        private void HandleStartGameResponse(Message msg)
        {
            var response = msg.Contents.ToString().Deserialize<StartGameResponse>();

            var requestingClient = ClientMacAddressToConnectionDictionary[response.RequestingClientMac];
            var respondingClient = ClientMacAddressToConnectionDictionary[response.RespondingClientMac];

            // Check if the request was accepted
            if (response.StartGameAccepted)
            {
                // Send join room message
                var room = new Room(Events, response.RequestingClientMac, response.RespondingClientMac);
                OpenRooms.Add(room.GetRoomId(), room);
                
                var joinMessage = new JoinRoomRequest(){RoomId = room.GetRoomId()};
                var message = new Message(){CommunicationMessageId = JoinRoomRequest.MessageId, Contents = joinMessage};
                MessageServer.Send(message.Serialize(), requestingClient, NetDeliveryMethod.ReliableUnordered);
                MessageServer.Send(message.Serialize(), respondingClient, NetDeliveryMethod.ReliableUnordered);
            }
            else
            {
                // send requesting client the denial message
            }

        }

        /// <summary>
        /// Handles the start game request from a client.
        /// Used to forward a request to a client from another client.
        /// </summary>
        /// <param name="msg"></param>
        private void HandleStartGameRequest(Message msg)
        {
            var request = msg.Contents.ToString().Deserialize<StartGameRequest>();
            var targetClient = GetConnectionFromMac(request.TargetClientMac);
            if (targetClient == null) return; // Or return failed to request game message  // TODO

            var forwardedMessage = new Message(){CommunicationMessageId = StartGameRequest.MessageId, Contents = request};
            MessageServer.Send(forwardedMessage.Serialize(), targetClient, NetDeliveryMethod.ReliableOrdered);
            Events.OnGuiLogMessageEvent($"Client {request.RequestingClientDisplayName} - {request.RequestingClientMac} is challenging client {ClientMacToDisplayNameDictionary[request.TargetClientMac]} - {request.TargetClientMac} to a match!");
        }

        private NetConnection GetConnectionFromMac(string mac)
        {
            ClientMacAddressToConnectionDictionary.TryGetValue(mac, out var connection);
            return connection;
        }
    }
}