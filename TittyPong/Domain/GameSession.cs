using Common.Game_Data;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Maths;

namespace TittyPong.Domain
{
    public class GameSession
    {

        public string ClientAId { get; set; }//left
        public string ClientBId { get; set; }//right

        public string ClientADisplayName { get; set; }
        public string ClientBDisplayName { get; set; }

        public bool IsClientA;

        public Guid RoomId { get; set; }

        public GameState State;

        public GameSession(string clientAId, string clientBId)
        {
            ClientAId = clientAId;
            ClientBId = clientBId;
            
            IsClientA = ClientAId == NET.Client.ClientId;

            State = new GameState
            {
                ClientA = new Client
                {
                    Id = ClientAId,
                    Body = new Circle(100f, 100f, 32)
                },
                ClientB = new Client
                {
                    Id = ClientBId,
                    Body = new Circle(1754f, 100f, 32)
                },
                Nipple = new Pong
                {
                    Force = new Vector2(),
                    Body = new Circle(1920 / 2, 1080 / 2, 8)
                }
            };
        }


        public Client GetThisClient()
        {
            return IsClientA ? State.ClientA : State.ClientB;
        }
    }
}
