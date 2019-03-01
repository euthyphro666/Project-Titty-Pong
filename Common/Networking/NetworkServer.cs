using System;
using System.Diagnostics;
using Lidgren.Network;

namespace Common.Networking
{
    public class NetworkServer : INetworkSocket
    {
        private NetServer Server;
        private ReceivedCallback Callback;

        public NetworkServer()
        {
            var config = new NetPeerConfiguration("TittyPong");
            config.Port = 54555;
            
            Server = new NetServer(config);
            Server.RegisterReceivedCallback(Received);
        }

        private void Received(object state)
        {
            var server = state as NetServer;
            var msg = server?.ReadMessage();
            switch (msg?.MessageType)
            {
                case NetIncomingMessageType.Error:
                case NetIncomingMessageType.StatusChanged:
                case NetIncomingMessageType.UnconnectedData:
                case NetIncomingMessageType.ConnectionApproval:
                case NetIncomingMessageType.Data:
                    var data = msg?.ReadBytes(msg.LengthBytes);

                    Debug.WriteLine("Received Data message type");
                    if (data != null)
                    {
                        Callback(data);
                    }
                    
                    break;
                case NetIncomingMessageType.Receipt:
                case NetIncomingMessageType.DiscoveryRequest:
                case NetIncomingMessageType.DiscoveryResponse:
                case NetIncomingMessageType.VerboseDebugMessage:
                case NetIncomingMessageType.DebugMessage:
                case NetIncomingMessageType.WarningMessage:
                case NetIncomingMessageType.ErrorMessage:
                case NetIncomingMessageType.NatIntroductionSuccess:
                case NetIncomingMessageType.ConnectionLatencyUpdated:
                default:
                    Debug.WriteLine("Unrecognized message type: " + msg?.MessageType);
                    break;
            }

        }

        public void Start(ReceivedCallback callback)
        {
            Callback = callback;
            Server.Start();
        }

        public void Connect(string ip)
        {
        }

        public void Send(byte[] data)
        {
            var msg = Server.CreateMessage();
            msg.Write(data);
            Server.SendToAll(msg, NetDeliveryMethod.Unreliable);
        }
    }
}