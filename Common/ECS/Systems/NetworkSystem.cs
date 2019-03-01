using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Common.ECS.Contracts;
using Common.ECS.Nodes;
using Common.ECS.SystemEvents;
using Common.Networking;

namespace Common.ECS.Systems
{
    public class NetworkSystem : ISystem
    {
        public uint Priority { get; set; }
        private readonly ISystemContext SystemContext;
        private readonly INetworkSocket Socket;
        private readonly List<NetworkInputNode> NetworkInputNodes;

        public NetworkSystem(ISystemContext systemContext, INetworkSocket socket)
        {
            SystemContext = systemContext;
            Socket = socket;

            NetworkInputNodes = new List<NetworkInputNode>();

            SystemContext.Events.InputEvent += OnInputEvent;
            
            socket.Start(ReceivedNetworkMessage);
            socket.Connect("192.168.1.223"); // TODO make this not hardcoded
        }

        private void ReceivedNetworkMessage(byte[] data)
        {
            // parse data to whatever type we need and handle it
        }

        public void Update()
        {
            // Send the input nodes
            if (NetworkInputNodes.Any())
            {
                //Socket.Send();
            }
        }

        private void OnInputEvent(object sender, InputEventArgs e)
        {
            Socket.Send(Encoding.UTF8.GetBytes("Input: " + e.Input.Value));
            //NetworkInputNodes.Add(new NetworkInputNode() {Player = e.Player, FrameInput = e.Input, FrameNumber = e.Frame});
        }
    }
}