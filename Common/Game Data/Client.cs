using Microsoft.Xna.Framework;
using System;
using Common.Maths;

namespace Common.Game_Data
{
    public class Client
    {
        public string Id { get; set; }
        public Circle Body { get; set; }

        public Client() { }
        public Client(Client client)
        {
            Id = client.Id;
            Body = client.Body;
        }
    }
}