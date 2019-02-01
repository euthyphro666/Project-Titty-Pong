using System;
using System.Threading;
using Common.Game_Data;
using Common.Messages;
using Microsoft.Xna.Framework;

namespace TittyPongServer.Game_Room
{
    public class GameSession
    {
        private readonly Events Events;
        private readonly Guid RoomId;
        private readonly Player ClientA;
        private readonly Player ClientB;
        private readonly Pong Nipple;

        private Timer GameTimer;
        private Thread GameThread;

        private const int UpdateTimeStep = 1000 / 20; // 1 second / times per second

        public GameSession(Events events, Guid roomId, string clientAId, string clientBId)
        {
            Events = events;
            RoomId = roomId;
            ClientA = new Player(clientAId) {PlayerClient = {Position = new Vector2(100, 100)}};
            ClientB = new Player(clientBId) {PlayerClient = {Position = new Vector2(1754, 100)}};
            Nipple = new Pong {Position = new Vector2(1920 / 2, 1080 / 2)};

            GameThread = new Thread(GameThreadStart);
        }

        private void GameThreadStart()
        {
            GameTimer = new Timer(Update);
            GameTimer.Change(UpdateTimeStep, 0);
        }

        public void Start()
        {
            GameThread.Start();
            Events.OnGuiLogMessageEvent($"Starting game for clients: {ClientA.PlayerId()} and {ClientB.PlayerId()}");
        }

        // The main game loop
        public void QueueInput(GameInputUpdate update)
        {
            if (update.ClientId == ClientA.PlayerId())
            {
                ClientA.QueueInput(update.Input);
                Events.OnGuiLogMessageEvent($"Client A: Direction: {update.Input} Input Number: {update.Input.InputNumber}");
            }
            else if (update.ClientId == ClientB.PlayerId())
            {
                ClientB.QueueInput(update.Input);
                Events.OnGuiLogMessageEvent($"Client B: Direction: {update.Input} Input Number: {update.Input.InputNumber}");
            }
        }

        // Reads input messages and applies transforms to client objects
        // Raises event to send positions to clients 60 times a second
        private void Update(object sender)
        {
            ClientA.Update();
            ClientB.Update();

            // send results
            var clientAState = new GameState()
                {ClientA = ClientA.PlayerClient, ClientB = ClientB.PlayerClient, Nipple = Nipple};

            clientAState.LastProcessedInputNumber = ClientA.LastProcessedInputNumber;
            
            var clientBState= new GameState()
                {ClientA = ClientA.PlayerClient, ClientB = ClientB.PlayerClient, Nipple = Nipple};
            clientBState.LastProcessedInputNumber = ClientB.LastProcessedInputNumber;

            Events.OnUpdateClientsEvent(new UpdateClientsEventArgs()
                {RoomId = RoomId, ClientAState = clientAState, ClientBState = clientBState});
            GameTimer.Change(UpdateTimeStep, 0);
            
            
            Events.OnGuiLogMessageEvent($"Update: ClientA Last Input = {ClientA.LastProcessedInputNumber} ClientB Last Input = {ClientB.LastProcessedInputNumber}");
        }
    }
}