using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Common;
using Common.Game_Data;
using Common.Messages;
using Microsoft.Xna.Framework;

namespace TittyPongServer.Game_Room
{
    public class Player
    {
        public long LastProcessedTimestamp { get; private set; }
        private readonly ConcurrentQueue<InputState> Inputs;
        private static int MoveSpeed = 20;

        public Client PlayerClient { get; set; }

        public Player(string id)
        {
            PlayerClient = new Client() {Id = id};
            Inputs = new ConcurrentQueue<InputState>();
        }

        public void QueueInput(List<InputState> inputs)
        {
            foreach (var input in inputs)
            {
                Inputs.Enqueue(input);
            }
        }

        public string PlayerId()
        {
            return PlayerClient.Id;
        }

        public void Update()
        {
            var sum = 0;
            while (Inputs.Count > 0)
            {
                Inputs.TryDequeue(out var inputUpdate);
                if (inputUpdate == null) return;

                var scale = inputUpdate.State == Direction.Up ? -1 : 1;

                sum += scale;
                
                LastProcessedTimestamp = inputUpdate.Timestamp;
            }
            PlayerClient.Body.Position = new Vector2(PlayerClient.Body.Position.X, PlayerClient.Body.Position.Y + sum * MoveSpeed);
        }
    }
}