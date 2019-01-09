using System;
using Common;
using Common.Messages;
using Lidgren.Network;
using TittyPong.Events;

namespace TittyPong.NET
{
    public class Client
    {
        private readonly EventManager Events;
        private NetClient LidgrenClient;

        public event EventHandler<ReceivedMessageEventArgs> ReceivedMessageEvent;
        
        public Client(EventManager events)
        {
            Events = events;
            var config = new NetPeerConfiguration("TittyPong")
            {
                NetworkThreadName = "TittyPong - Network Thread"
            };
            LidgrenClient = new NetClient(config);
            LidgrenClient.Start();
            
            LidgrenClient.RegisterReceivedCallback(ReceivedMessage);
            Events.SendMessageEvent += HandleSendMessageEvent;
        }

        private void HandleSendMessageEvent(object sender, ByteArrayEventArgs e)
        {
            Send(e.Data, e.DeliveryMethod);
        }

        private void ReceivedMessage(object client)
        {
            var msg = (client as NetClient)?.ReadMessage();

            if (msg == null) return;
            
            var args = new ReceivedMessageEventArgs()
            {
                ReceivedMessage = msg
            };
            
            ReceivedMessageEvent?.Invoke(this, args);
        }

        public void Connect(string address, int port)
        {
            LidgrenClient.Connect(address, port);
        }

        public NetConnectionStatus Status()
        {
            return LidgrenClient.ConnectionStatus;
        }

        /// <summary>
        /// Send a message to the server.
        /// </summary>
        /// <param name="msg">The outgoing message to send. Use 'CreateMessage' to instantiate a message object first.</param>
        /// <param name="method">The delivery method. Default: NetDeliveryMethod.Unreliable</param>
        /// <returns>True if the message was delivered, false if not</returns>
        public bool Send(NetOutgoingMessage msg, NetDeliveryMethod method = NetDeliveryMethod.Unreliable)
        {
            if (Status() != NetConnectionStatus.Connected)
                return false;
            var result = LidgrenClient.SendMessage(msg, method);
            return result == NetSendResult.Sent;
        }
        
        /// <summary>
        /// Send a message to the server.
        /// </summary>
        /// <param name="msg">The outgoing message to send. Use 'CreateMessage' to instantiate a message object first.</param>
        /// <param name="method">The delivery method. Default: NetDeliveryMethod.Unreliable</param>
        /// <returns>True if the message was delivered, false if not</returns>
        public bool Send(byte[] msg, NetDeliveryMethod method = NetDeliveryMethod.Unreliable)
        {
            var outgoingMessage = CreateMessage();
            outgoingMessage.Write(msg);
            if (Status() != NetConnectionStatus.Connected)
                return false;
            var result = LidgrenClient.SendMessage(outgoingMessage, method);
            return result == NetSendResult.Sent;
        }

        public NetOutgoingMessage CreateMessage()
        {
            return LidgrenClient.CreateMessage();
        }
    }
}