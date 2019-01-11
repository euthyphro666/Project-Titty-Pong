using System;
using Common;
using Common.Messages;
using Lidgren.Network;
using TittyPong.Events;

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
                case MessageIds.ConnectionResponse:
                    var response = msg.Contents.ToString().Deserialize<ConnectionResponse>();
                    //response.AvailableClients
                    break;
                default:
                    // Log that we received an unhandled data message
//                    Events.
                    break;
            }
        }
    }
}