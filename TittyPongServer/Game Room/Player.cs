using System;
using System.Collections.Generic;
using Common;
using Common.Game_Data;
using Microsoft.Xna.Framework;

namespace TittyPongServer.Game_Room
{
    public class Player
    {
        private readonly Queue<InputState> Inputs;

        public Client PlayerClient { get; set; }
        
        public Player(string id)
        {
            PlayerClient = new Client(){Id = id};
            Inputs = new Queue<InputState>();
        }

        public void QueueInput(InputState input)
        {
            Inputs.Enqueue(input);
        }

        public InputState TryGetNextInput()
        {
            return Inputs.Count > 0 ? Inputs.Dequeue() : null;
        }

        public string PlayerId()
        {
            return PlayerClient.Id;
        }

        public void Update(InputState input)
        {
            switch (input.State)
            {
                case InputState.Direction.None:
                    break;
                case InputState.Direction.Up:
                    PlayerClient.Position = new Vector2(PlayerClient.Position.X, PlayerClient.Position.Y + 1);
                    break;
                case InputState.Direction.Down:
                    PlayerClient.Position = new Vector2(PlayerClient.Position.X, PlayerClient.Position.Y - 1);
                    break;
                case InputState.Direction.Left:
                    break;
                case InputState.Direction.Right:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}