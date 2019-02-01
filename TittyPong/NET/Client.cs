using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using Common;
using Common.Messages;
using Lidgren.Network;
using TittyPong.Events;

namespace TittyPong.NET
{
    public class Client
    {
        public static string ClientId => 
        (
        from nic in NetworkInterface.GetAllNetworkInterfaces()
            where nic.OperationalStatus == OperationalStatus.Up
            select nic.GetPhysicalAddress().ToString()
            ).FirstOrDefault();
        
        private readonly EventManager Events;
        private NetClient LidgrenClient;

        private const int ServerPort = 6969;
        
        public event EventHandler<ReceivedMessageEventArgs> ReceivedMessageEvent;
        
        public Client(EventManager events)
        {
            Events = events;
            var config = new NetPeerConfiguration("TittyPong")
            {
                NetworkThreadName = "TittyPong - Network Thread"
            };
            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            
            LidgrenClient = new NetClient(config);
            LidgrenClient.Start();
            
            LidgrenClient.RegisterReceivedCallback(ReceivedMessage);
            Events.SendMessageEvent += HandleSendMessageEvent;
            Events.ConnectEvent += HandleConnectEvent;
        }

        private void HandleConnectEvent(object sender, ConnectEventArgs e)
        {
           LidgrenClient.Connect(e.Ip, ServerPort);
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
            (client as NetClient)?.Recycle(msg);
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
        public bool Send(byte[] msg, NetDeliveryMethod method = NetDeliveryMethod.Unreliable)
        {
            var outgoingMessage = LidgrenClient.CreateMessage();
            outgoingMessage.Write(msg);
            if (Status() != NetConnectionStatus.Connected)
                return false;
            var result = LidgrenClient.SendMessage(outgoingMessage, method);
            LidgrenClient.FlushSendQueue();
            return result == NetSendResult.Sent;
        }

    }
}