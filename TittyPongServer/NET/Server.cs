using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Lidgren.Network;

namespace TittyPongServer.NET
{
    public class Server
    {
        private NetServer LidgrenServer;

        public event EventHandler<ReceivedMessageEventArgs> ReceivedMessageEvent;
        
        
        public Server()
        {
            var config = new NetPeerConfiguration("TittyPong")
            {
                Port = 9191
            };
            LidgrenServer = new NetServer(config);
            LidgrenServer.Start();
            
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            LidgrenServer.RegisterReceivedCallback(ReceivedMessageHandler);

            App.Events.RaiseGuiLogMessageEvent($"Server started at: {GetLocalIPAddress()}:{LidgrenServer.Port}");
        }

        private void ReceivedMessageHandler(object state)
        {
            var msg = (state as NetServer)?.ReadMessage();
            if (msg == null) return;
            var args = new ReceivedMessageEventArgs(){ReceivedMessage = msg};
            
            ReceivedMessageEvent?.Invoke(this, args);
        }

        private static string GetLocalIPAddress()
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