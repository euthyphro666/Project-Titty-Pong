using System.Timers;
using Common.Messages;

namespace TittyPongServer.Game_Room
{
    public class GameSession
    {
        private readonly Events Events;
        private readonly string ClientAId;
        private readonly string ClientBId;

        private Timer GameTimer;
        
        public GameSession(Events events, string clientAId, string clientBId)
        {
            Events = events;
            ClientAId = clientAId;
            ClientBId = clientBId;
            GameTimer = new Timer(17); // Roughly 60 times a second
            GameTimer.Elapsed += Update;
            GameTimer.AutoReset = true;
        }

        public void Start()
        {
            GameTimer.Start();
        }
        
        // The main game loop
        public void QueueInput(GameInputUpdate update)
        {
            update.ClientId;
            update.Input;
        }

        // Reads input messages and applies transforms to client objects
        // Raises event to send positions to clients 60 times a second
        private void Update(object sender, ElapsedEventArgs e)
        {
            // Apply input queue
            // send results
            Events.OnUpdateClientsEvent(new UpdateClientsEventArgs(){ClientAId = ClientAId, ClientBId = ClientBId, ClientAPosition = ??, ClientBPosition = ??, PongPosition = ??});
        }
    }
}