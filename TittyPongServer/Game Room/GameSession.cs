using System;
using System.Timers;
using Common;
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

        public GameSession(Events events, Guid roomId, string clientAId, string clientBId)
        {
            Events = events;
            RoomId = roomId;
            ClientA = new Player(clientAId) {PlayerClient = {Position = new Vector2(100, 100)}};
            ClientB = new Player(clientBId) {PlayerClient = {Position = new Vector2(1754, 100)}};
            Nipple = new Pong {Position = new Vector2(1920 / 2, 1080 / 2)};

            GameTimer = new Timer(17); // Roughly 30 times a second
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
                ClientA.Update();
                ClientB.Update();

            // send results
            var state = new GameState()
                {ClientA = ClientA.PlayerClient, ClientB = ClientB.PlayerClient, Nipple = Nipple};

            Events.OnUpdateClientsEvent(new UpdateClientsEventArgs() {RoomId = RoomId, State = state});

        }
    }
}