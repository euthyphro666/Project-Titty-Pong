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

        public MessageProducer(EventManager events)
        {
            Events = events;

            Events.ConnectionInfoEvent += HandleConnectionInfoEvent;
            Events.ConnectSuccessEvent += HandleConnectSuccessEvent;
        }

        private void HandleConnectSuccessEvent(object sender, EventArgs e)
        {
            
            
            var request = new ConnectionRequest() {ClientId = Client.ClientId};
            var msg = new Message(){MessageId = ConnectionRequest.MessageId, Contents = request};
            Events.OnSendMessageEvent(this, new ByteArrayEventArgs(msg.Serialize()));
        }

        private void HandleConnectionInfoEvent(object sender, StringEventArgs e)
        {
            var ip = e.Data;
            Events.OnConnectEvent(this, new ConnectEventArgs(ip));
        }
    }
}