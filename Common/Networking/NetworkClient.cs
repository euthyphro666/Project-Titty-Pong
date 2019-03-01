using System;
using System.Diagnostics;
using Lidgren.Network;

namespace Common.Networking
{
    public class NetworkClient : INetworkSocket
    {
        private NetClient Client;

        public ReceivedCallback Callback;
        
        public NetworkClient()
        {
            var config = new NetPeerConfiguration("TittyPong");
            Client = new NetClient(config);
            Client.RegisterReceivedCallback(Received);
        }

        private void Received(object state)
        {
            var client = state as NetClient;
            var msg = client?.ReadMessage();
            switch (msg?.MessageType)
            {
                case NetIncomingMessageType.Error:
                case NetIncomingMessageType.StatusChanged:
                case NetIncomingMessageType.UnconnectedData:
                case NetIncomingMessageType.ConnectionApproval:
                case NetIncomingMessageType.Data:
                    var data = msg?.ReadBytes(msg.LengthBytes);

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
            Client.Start();
        }

        public void Connect(string ip)
        {
            Client.Connect(ip, 54555);
        }

        public void Send(byte[] data)
        {
            var msg = Client.CreateMessage();
            msg.Write(data);
            Client.SendMessage(msg, NetDeliveryMethod.Unreliable);
        }
    }
}