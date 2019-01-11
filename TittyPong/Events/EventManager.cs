using System;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using TittyPong.IO;

namespace TittyPong.Events
{
    public class EventManager
    {
        public event EventHandler<ClientListReceivedEventArgs> ClientListReceivedEvent;
        public event EventHandler<ConnectEventArgs> ConnectEvent;
        public event EventHandler<EventArgs> ConnectSuccessEvent;
        public event EventHandler<ByteArrayEventArgs> SendMessageEvent;
        public event EventHandler<StringEventArgs> ConnectionInfoEvent;

        public void OnConnectionInfoEvent(object sender, StringEventArgs e)
        {
            ConnectionInfoEvent?.Invoke(sender, e);
        }
        
        public void OnSendMessageEvent(object sender, ByteArrayEventArgs e)
        {
            SendMessageEvent?.Invoke(sender, e);
        }

        public void OnConnectEvent(object sender, ConnectEventArgs connectEventArgs)
        {
            ConnectEvent?.Invoke(sender, connectEventArgs);
        }

        public void OnConnectSuccessEvent(object sender)
        {
            ConnectSuccessEvent?.Invoke(sender, EventArgs.Empty);
        }

        public void OnClientListReceived(object sender,ClientListReceivedEventArgs e)
        {
            ClientListReceivedEvent?.Invoke(sender, e);
        }
    }
}
