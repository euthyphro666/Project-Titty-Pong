using System.Collections.Generic;
using System.Linq;
using Common.ECS.Contracts;
using Common.ECS.Nodes;
using Common.ECS.SystemEvents;

namespace Common.ECS.Systems
{
    public class NetworkSystem : ISystem
    {
        public uint Priority { get; set; }
        private readonly ISystemContext SystemContext;
        private readonly List<NetworkInputNode> NetworkInputNodes;

        public NetworkSystem(ISystemContext systemContext)
        {
            SystemContext = systemContext;
            
            NetworkInputNodes = new List<NetworkInputNode>();

            SystemContext.Events.InputEvent += OnInputEvent;
        }

        public void Update()
        {
            // Send the input nodes
            if (NetworkInputNodes.Any())
            {
                
            }
        }

        private void OnInputEvent(object sender, InputEventArgs e)
        {
            NetworkInputNodes.Add(new NetworkInputNode() {Player = e.Player, FrameInput = e.Input, FrameNumber = e.Frame});
        }
    }
}