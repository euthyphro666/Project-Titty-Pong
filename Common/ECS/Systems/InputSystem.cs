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
        public uint Priority { get; set; }
        private readonly ISystemContext SystemContext;
        private IEventManager Events;

        public InputSystem(ISystemContext systemContext)
        {
            SystemContext = systemContext;
            Events = SystemContext.Events;
        }

        public void Update()
        {
            //Only consider keyboard input for now
            var state = Keyboard.GetState();
            var up = state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up);
            var down = state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down);
            var input = (byte)((up ^ down) ? (up ? -1 : 1) : 0);
            Events.RaiseInputEvent(input);

        }

    }
}
