using Common.Game_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TittyPong.Events.Args
{
    public class GameStateArgs : EventArgs
    {
        public GameState State { get; set; }
    }
}
