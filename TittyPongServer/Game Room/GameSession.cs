using System.Timers;
using Common;
using Common.Game_Data;
using Common.Messages;

namespace TittyPongServer.Game_Room
{
    public class GameSession
    {
        private readonly Events Events;
        private readonly Player ClientA;
        private readonly Player ClientB;
        private readonly Pong Nipple;

        private Timer GameTimer;
        
        public GameSession(Events events, string clientAId, string clientBId)
        {
            Events = events;
            ClientA = new Player(clientAId);
            ClientB = new Player(clientBId);
            Nipple = new Pong();
            
            GameTimer = new Timer(17); // Roughly 60 times a second
            GameTimer.Elapsed += Update;
            GameTimer.AutoReset = true;
        }

        public void Start()
        {
            GameTimer.Start();
            Events.OnGuiLogMessageEvent($"Starting game for clients: {ClientA.PlayerId()} and {ClientB.PlayerId()}");
        }
        
        // The main game loop
        public void QueueInput(GameInputUpdate update)
        {
            if (update.ClientId == ClientA.PlayerId())
            {
                ClientA.QueueInput(update.Input);
            }
            else if (update.ClientId == ClientB.PlayerId())
            {
                ClientB.QueueInput(update.Input);
            }
        }

        // Reads input messages and applies transforms to client objects
        // Raises event to send positions to clients 60 times a second
        private void Update(object sender, ElapsedEventArgs e)
        {
            // Apply input queue
            // send results
            var state = new GameState(){ClientA = ClientA.PlayerClient, ClientB = ClientB.PlayerClient, Nipple = Nipple};
          Events.OnUpdateClientsEvent(new UpdateClientsEventArgs(){State = state});
          
          Events.OnGuiLogMessageEvent($"Update client inputs: \nClient A: {ClientA.PlayerId()} Input: {ClientA.TryGetNextInput().Serialize().DeserializeToJsonString()} \nClient B: {ClientB.PlayerId()} Input: {ClientB.TryGetNextInput().Serialize().DeserializeToJsonString()}");
        }
    }
}