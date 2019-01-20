using System;
using Common;
using Common.Messages;
using Lidgren.Network;
using TittyPong.Events;
using TittyPong.Events.Args;

namespace TittyPong.NET
{
    public class MessageConsumer
    {
        private readonly EventManager Events;

        public  MessageConsumer(EventManager events)
        {
            Events = events;
        }
        
        public void ConsumeMessage(object sender, ReceivedMessageEventArgs e)
        {
            var incomingMessage = e.ReceivedMessage;
            switch (incomingMessage.MessageType)
            {
                case NetIncomingMessageType.StatusChanged:
                    var status = (NetConnectionStatus)e.ReceivedMessage.ReadByte();
                    
                    if (status == NetConnectionStatus.Connected)
                        Events.OnConnectSuccessEvent(this);
                    break;
                case NetIncomingMessageType.Data:
                    var msg = e.ReceivedMessage.ReadBytes(e.ReceivedMessage.LengthBytes).Deserialize<Message>();
                    HandleDataMessage(msg);
                    break;
                default:
                    // Log that we received an unhandled message type from lidgren?
//                    Events.
                    break;
            }
        }

        private void HandleDataMessage(Message msg)
        {
            switch (msg.MessageId)
            {
                case CommunicationMessageIds.ConnectionResponse:
                    var response = msg.Contents.ToString().Deserialize<ConnectionResponse>();
                    
                    response.AvailableClients.Remove(Client.ClientId);
                    
                    Events.OnClientListReceived(this, new ClientListReceivedEventArgs(response.AvailableClients));
                    break;
                case CommunicationMessageIds.StartGameRequest:
                    HandleStartGameRequest(msg);
                    break;
                case CommunicationMessageIds.JoinRoomRequest:
                    var joinRoom = msg.Contents.ToString().Deserialize<JoinRoomRequest>();
                    Events.OnJoinRoomEvent(this, new JoinRoomEventArgs { RoomId = joinRoom.RoomId });
                    var reply = new Message
                    {
                        MessageId = CommunicationMessageIds.RoomMessage,
                        Contents = new RoomConfirmation
                        {
                            ClientMac = Client.ClientId,
                            RoomId = joinRoom.RoomId
                        }
                    };
                    Events.OnSendMessageEvent(this, new ByteArrayEventArgs(reply.Serialize()));
                    break;
                case CommunicationMessageIds.RoomMessage:
                    var roomMsg = msg.Contents.ToString().Deserialize<RoomMessage>();
                    HandleRoomMessage(roomMsg);
                    break;
                default:
                    // Log that we received an unhandled data message
//                    Events.
                    break;
            }
        }
        private void HandleRoomMessage(RoomMessage msg)
        {
            switch (msg.RoomMessageId)
            {
                case RoomMessageIds.RoomConfirmation:
                    break;
                case RoomMessageIds.GameInputUpdate:
                    break;
                default:
                    // Log that we received an unhandled data message
                    //                    Events.
                    break;
            }
        }



        private void HandleStartGameRequest(Message msg)
        {
            var request = msg.Contents.ToString().Deserialize<StartGameRequest>();
            Events.OnReceivedStartGameRequestEvent(this, new ReceivedStartGameRequestEventArgs(){RequestingClientMac = request.RequestingClientMac, RequestingClientDisplayName = request.RequestingClientDisplayName});
        }
    }
}