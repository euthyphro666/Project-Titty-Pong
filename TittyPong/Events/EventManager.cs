using System;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using TittyPong.Events.Args;
using TittyPong.IO;

namespace TittyPong.Events
{
    public class EventManager
    {

        public event EventHandler<GameStateArgs> RoomUpdateEvent;
        public void OnRoomUpdateEvent(object sender, GameStateArgs e)
        {
            RoomUpdateEvent?.Invoke(sender, e);
        }

        public event EventHandler<InputEventArgs> InputEvent;
        public void OnInputEvent(object sender, InputEventArgs e)
        {
            InputEvent?.Invoke(sender, e);
        }

        public event EventHandler<InputEventArgs> SendInputEvent;
        public void OnSendInputEvent(object sender, InputEventArgs e)
        {
            SendInputEvent?.Invoke(sender, e);
        }

        public event EventHandler<StringEventArgs> LoggingEvent;
        public void OnLoggingEvent(object sender, StringEventArgs e)
        {
            LoggingEvent?.Invoke(sender, e);
        }

        public event EventHandler<JoinRoomEventArgs> JoinRoomEvent;
        public void OnJoinRoomEvent(object sender, JoinRoomEventArgs e)
        {
            JoinRoomEvent?.Invoke(sender, e);
        }

        public event EventHandler<ChangeStateEventArgs> ChangeStateEvent;
        public void OnChangeStateEvent(object sender, ChangeStateEventArgs e)
        {
            ChangeStateEvent?.Invoke(sender, e);
        }


        public event EventHandler<StartGameResponseEventArgs> StartGameResponseEvent;
        public void OnStartGameResponseEvent(object sender, StartGameResponseEventArgs e)
        {
            StartGameResponseEvent?.Invoke(sender, e);
        }

        public event EventHandler<StringEventArgs> StartGameRequestEvent;
        public void OnStartGameRequestEvent(object sender, StringEventArgs e)
        {
            StartGameRequestEvent?.Invoke(sender, e);
        }

        public event EventHandler<ConnectionInfoEventArgs> ConnectionInfoEvent;
        public void OnConnectionInfoEvent(object sender, ConnectionInfoEventArgs e)
        {
            ConnectionInfoEvent?.Invoke(sender, e);
        }

        public event EventHandler<ByteArrayEventArgs> SendMessageEvent;
        public void OnSendMessageEvent(object sender, ByteArrayEventArgs e)
        {
            SendMessageEvent?.Invoke(sender, e);
        }

        public event EventHandler<ConnectEventArgs> ConnectEvent;
        public void OnConnectEvent(object sender, ConnectEventArgs connectEventArgs)
        {
            ConnectEvent?.Invoke(sender, connectEventArgs);
        }

        public event EventHandler<EventArgs> ConnectSuccessEvent;
        public void OnConnectSuccessEvent(object sender)
        {
            ConnectSuccessEvent?.Invoke(sender, EventArgs.Empty);
        }

        public event EventHandler<ClientListReceivedEventArgs> ClientListReceivedEvent;
        public void OnClientListReceived(object sender,ClientListReceivedEventArgs e)
        {
            ClientListReceivedEvent?.Invoke(sender, e);
        }

        public event EventHandler<ReceivedStartGameRequestEventArgs> ReceivedStartGameRequestEvent;
        public void OnReceivedStartGameRequestEvent(object sender, ReceivedStartGameRequestEventArgs e)
        {
            ReceivedStartGameRequestEvent?.Invoke(sender, e);
        }

    }
}
