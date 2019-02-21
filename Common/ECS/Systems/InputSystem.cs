using Common.ECS.Contracts;
using Common.ECS.Nodes;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ECS.Systems
{
    public class InputSystem : ISystem
    {

        private List<InputNode> Nodes;
        private IEventManager Events;

        public InputSystem(IEventManager events)
        {
            Events = events;
        }

        public void Update()
        {
            var state = Keyboard.GetState();
        }
    }
}
