using Common.Game_Data;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    Position = new Vector2(100f, 100f)
                },
                ClientB = new Client
                {
                    Id = ClientBId,
                    Position = new Vector2(1754f, 100f)
                },
                Nipple = new Pong
                {
                    Force = new Vector2(),
                    Position = new Vector2(1920 / 2, 1080 / 2)
                }
            };
        }


        public Client GetThisClient()
        {
            return IsClientA ? State.ClientA : State.ClientB;
        }
    }
}
