using Microsoft.Xna.Framework;
using System;

namespace Common.Game_Data
{
    public class Client
    {
        public string Id { get; set; }
        public Vector2 Position { get; set; }

        public Client() { }
        public Client(Client client)
        {
            Id = client.Id;
            Position = new Vector2(client.Position.X, client.Position.Y);
        }
    }
}