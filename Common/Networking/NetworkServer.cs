using System;
using System.Diagnostics;
using System.Text;
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
                case NetIncomingMessageType.Data:
                    var data = msg?.ReadBytes(msg.LengthBytes);
                    if (data != null)
                    {
                        Debug.Write("Received Data message type: ");
                        Debug.WriteLine(Encoding.UTF8.GetString(data));


                        Callback(data);
                    }

                    break;
//                case NetIncomingMessageType.Error:
//                case NetIncomingMessageType.StatusChanged:
//                case NetIncomingMessageType.UnconnectedData:
//                case NetIncomingMessageType.ConnectionApproval:
//                case NetIncomingMessageType.Receipt:
//                case NetIncomingMessageType.DiscoveryRequest:
//                case NetIncomingMessageType.DiscoveryResponse:
//                case NetIncomingMessageType.VerboseDebugMessage:
//                case NetIncomingMessageType.DebugMessage:
//                case NetIncomingMessageType.WarningMessage:
//                case NetIncomingMessageType.ErrorMessage:
//                case NetIncomingMessageType.NatIntroductionSuccess:
//                case NetIncomingMessageType.ConnectionLatencyUpdated:
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
            if (data == null) return;
            var msg = Server.CreateMessage();
            msg.Write(data);
            Server.SendToAll(msg, NetDeliveryMethod.Unreliable);
        }
    }
}