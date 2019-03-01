using Common.ECS.Components;
using Common.ECS.Contracts;

namespace Common.ECS.Nodes
{
    public class NetworkInputNode : INode
    {
        public PlayerNumber Player { get; set; }
        public int FrameNumber { get; set; }
        public Input FrameInput { get; set; }
    }
}