using Common.ECS.Components;
using Common.ECS.Contracts;
using Common.ECS.Nodes;
using Common.IO;
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
            var up = state.IsKeyDown(Keys.W);
            var down = state.IsKeyDown(Keys.S);
            var input = (Input)((up ^ down) ? (up ? -10 : 10) : 0);
            if(input != 0) Events.RaiseInputEvent(PlayerNumber.One, input, Engine.FrameNumber);

            // TODO only do this one if we're not online
            up = state.IsKeyDown(Keys.Up);
            down = state.IsKeyDown(Keys.Down);
            input = (Input)((up ^ down) ? (up ? -10 : 10) : 0);
            if (input != 0) Events.RaiseInputEvent(PlayerNumber.Two, input, Engine.FrameNumber);
        }

    }
}
