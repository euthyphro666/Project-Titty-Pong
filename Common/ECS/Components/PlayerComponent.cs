using Common.ECS.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ECS.Components
{
    public enum PlayerNumber
    {
        One,
        Two
    }
    public class PlayerComponent : IComponent
    {
        public PlayerNumber Number { get; set; }

    }
}
