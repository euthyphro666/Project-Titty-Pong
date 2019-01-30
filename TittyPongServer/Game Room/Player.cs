using System.Collections.Generic;

namespace TittyPongServer.Game_Room
{
    public class Player
    {
        private string Id { get; set; }
        private readonly Queue<object> Inputs;

        public Player(string id)
        {
            Id = id;
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
            return Id;
        }
    }
}