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
            while (Inputs.Count > 0)
            {
                Inputs.TryDequeue(out var inputUpdate);
                if (inputUpdate == null) return;
                
                switch (inputUpdate.State)
                {
                    case InputState.Direction.None:
                        break;
                    case InputState.Direction.Up:
                        PlayerClient.Position =
                            new Vector2(PlayerClient.Position.X, PlayerClient.Position.Y - MoveSpeed);
                        break;
                    case InputState.Direction.Down:
                        PlayerClient.Position =
                            new Vector2(PlayerClient.Position.X, PlayerClient.Position.Y + MoveSpeed);
                        break;
                    case InputState.Direction.Left:
                        break;
                    case InputState.Direction.Right:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                LastProcessedInputNumber = inputUpdate.InputNumber;
            }
        }
    }
}