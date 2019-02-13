using System;
using System.Diagnostics;
using System.Threading;
using Common.Game_Data;
using Common.Maths;
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

        private Stopwatch Time;
        
        private long CurrentTick;

        private const int UpdateTimeStep = 1000 / 33; // 1 second / times per second

        public GameSession(Events events, Guid roomId, string clientAId, string clientBId)
        {
            Events = events;
            RoomId = roomId;
            ClientA = new Player(clientAId) {PlayerClient = {Body = new Circle(100, 100, 32)}};
            ClientB = new Player(clientBId) {PlayerClient = {Body = new Circle(1754, 100, 32)}};
            Nipple = new Pong {Body = new Circle(1920 / 2, 1080 / 2, 8), Force = new Vector2(10,10)};
            CurrentTick = 0;
            GameThread = new Thread(GameThreadStart);
        }

        private void GameThreadStart()
        {
            GameTimer = new Timer(Update, null, 0, UpdateTimeStep);
        }

        public void Start()
        {
            GameThread.Start();
            
            Time = Stopwatch.StartNew();

            CurrentTick = Time.ElapsedMilliseconds;
            Events.OnStartGameEvent(RoomId, ClientA.PlayerId(), ClientB.PlayerId(), CurrentTick);
            
            Events.OnGuiLogMessageEvent($"Starting game for clients: {ClientA.PlayerId()} and {ClientB.PlayerId()}");
        }

        // The main game loop
        public void QueueInput(GameInputUpdate update)
        {
            if (update.ClientId == ClientA.PlayerId())
            {
                ClientA.QueueInput(update.Inputs);
                //Events.OnGuiLogMessageEvent($"Client A: Direction: {update.Inputs} Input Number: {update.Inputs.InputNumber}");
            }
            else if (update.ClientId == ClientB.PlayerId())
            {
                ClientB.QueueInput(update.Inputs);
                //Events.OnGuiLogMessageEvent($"Client B: Direction: {update.Inputs} Input Number: {update.Inputs.InputNumber}");
            }
        }

        // Reads input messages and applies transforms to client objects
        // Raises event to send positions to clients 60 times a second
        private void Update(object sender)
        {
            CurrentTick = Time.ElapsedMilliseconds;
            
            ClientA.Update();
            ClientB.Update();

            Nipple.Update(ClientA.PlayerClient.Body, ClientB.PlayerClient.Body);
            
            // send results
            var clientAState = new GameState()
                {ClientA = ClientA.PlayerClient, ClientB = ClientB.PlayerClient, Nipple = Nipple};

            clientAState.LastProcessedInputNumber = ClientA.LastProcessedInputNumber;
            
            var clientBState= new GameState()
                {ClientA = ClientA.PlayerClient, ClientB = ClientB.PlayerClient, Nipple = Nipple};
            clientBState.LastProcessedInputNumber = ClientB.LastProcessedInputNumber;

            Events.OnUpdateClientsEvent(new UpdateClientsEventArgs()
                {RoomId = RoomId, ClientAState = clientAState, ClientBState = clientBState, NetworkTimeSync = CurrentTick});
            
            Events.OnGuiLogMessageEvent($"Update: ClientA Last Input = {ClientA.LastProcessedInputNumber} ClientB Last Input = {ClientB.LastProcessedInputNumber}");
        }
    }
}