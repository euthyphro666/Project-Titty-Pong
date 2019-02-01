using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Common;
using Lidgren.Network;

namespace TittyPongServer.NET
{
    public class Server
    {
        private readonly Events Events;
        private NetServer LidgrenServer;

        public event EventHandler<ReceivedMessageEventArgs> ReceivedMessageEvent;
        
        public Server(Events events)
        {
            Events = events;
            var config = new NetPeerConfiguration("TittyPong")
            {
                Port = 6969
            };
            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            LidgrenServer = new NetServer(config);
            LidgrenServer.Start();
            
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            LidgrenServer.RegisterReceivedCallback(ReceivedMessageEventHandler);

            Events.OnGuiLogMessageEvent($"Server started at: {GetLocalIpAddress()}:{LidgrenServer.Port}");
        }


        public bool Send(byte[] msg, NetConnection client, NetDeliveryMethod method = NetDeliveryMethod.Unreliable)
        {
            var outgoingMessage = CreateMessage();
            outgoingMessage.Write(msg);
            var result = LidgrenServer.SendMessage(outgoingMessage, client, method);
            return result == NetSendResult.Sent;
        }
        
        public void Broadcast(byte[] msg, NetDeliveryMethod method = NetDeliveryMethod.Unreliable)
        {
            var outgoingMessage = CreateMessage();
            outgoingMessage.Write(msg);
            LidgrenServer.SendToAll(outgoingMessage, method);
        }
        
        public NetOutgoingMessage CreateMessage()
        {
            return LidgrenServer.CreateMessage();
        }
        
        private void ReceivedMessageEventHandler(object state)
        {
            var msg = (state as NetServer)?.ReadMessage();
            if (msg == null) return;
            var args = new ReceivedMessageEventArgs(){ReceivedMessage = msg};
            
            ReceivedMessageEvent?.Invoke(this, args);
            (state as NetServer)?.Recycle(msg);
        }

        private static string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        
    }
}