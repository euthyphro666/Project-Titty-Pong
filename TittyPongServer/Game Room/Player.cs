using System.Collections.Generic;
using Common.Game_Data;

namespace TittyPongServer.Game_Room
{
    public class Player
    {
        private readonly Queue<object> Inputs;

        public Client PlayerClient { get; set; }
        
        public Player(string id)
        {
            PlayerClient = new Client(){Id = id};
            Inputs = new Queue<object>();
        }

        public void QueueInput(object input)
        {
            Inputs.Enqueue(input);
        }

        public object TryGetNextInput()
        {
            return Inputs.Count > 0 ? Inputs.Dequeue() : null;
        }

        public string PlayerId()
        {
            return PlayerClient.Id;
        }
    }
}