using System;
using System.Linq;
using System.Net.NetworkInformation;
using Common;
using Common.Messages;
using TittyPong.Events;

namespace TittyPong.NET
{
    public class MessageProducer
    {
        private EventManager Events;

        private string displayName;
        
        
        public MessageProducer(EventManager events)
        {
            Events = events;

            Events.ConnectionInfoEvent += HandleConnectionInfoEvent;
            Events.ConnectSuccessEvent += HandleConnectSuccessEvent;
            Events.StartGameRequestEvent += HandleStartGameRequestEvent;
            Events.StartGameResponseEvent += HandleStartGameResponseEvent;
        }

        private void HandleStartGameResponseEvent(object sender, StartGameResponseEventArgs e)
        {
            var msg = new StartGameResponse
            {
                RequestingClientMac = e.RequestingClientMac,
                RespondingClientMac = e.RespondingClientMac,
                StartGameAccepted = e.StartGameAccepted
            };
            Events.OnSendMessageEvent(this, new ByteArrayEventArgs(msg.Serialize()));
        }

        /// <summary>
        /// Sends a request to the server to start a game with another client.
        /// The other client will receive a start game request event and must accept or deny the request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleStartGameRequestEvent(object sender, StringEventArgs e)
        {
            var target = e.Data;
            var request = new StartGameRequest()
                {TargetClientMac = target, RequestingClientMac = Client.ClientId, RequestingClientDisplayName = displayName};
            var msg = new Message(){MessageId = StartGameRequest.MessageId, Contents = request};
            Events.OnSendMessageEvent(this, new ByteArrayEventArgs(msg.Serialize()));
        }

        private void HandleConnectSuccessEvent(object sender, EventArgs e)
        {
            var request = new ConnectionRequest() {ClientId = Client.ClientId, DisplayName = displayName};
            var msg = new Message(){MessageId = ConnectionRequest.MessageId, Contents = request};
            Events.OnSendMessageEvent(this, new ByteArrayEventArgs(msg.Serialize()));
        }

        private void HandleConnectionInfoEvent(object sender, ConnectionInfoEventArgs e)
        {
            var ip = e.Address;
            displayName = e.DisplayName;
            Events.OnConnectEvent(this, new ConnectEventArgs(ip));
        }
    }
}