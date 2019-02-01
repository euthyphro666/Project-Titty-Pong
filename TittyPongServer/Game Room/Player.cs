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
        public int LastProcessedInputNumber { get; private set; }
        private readonly ConcurrentQueue<InputState> Inputs;
        private static int MoveSpeed = 5;

        public Client PlayerClient { get; set; }

        public Player(string id)
        {
            PlayerClient = new Client() {Id = id};
            Inputs = new ConcurrentQueue<InputState>();
        }

        public void QueueInput(InputState input)
        {
            Inputs.Enqueue(input);
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

                var scale = inputUpdate.State == InputState.Direction.Up ? -1 : 1;

                sum += scale;
                
                LastProcessedInputNumber = inputUpdate.InputNumber;
            }
            PlayerClient.Position = new Vector2(PlayerClient.Position.X, PlayerClient.Position.Y + sum * MoveSpeed);
        }
    }
}