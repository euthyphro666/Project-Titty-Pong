using System.Collections.Generic;
using Common.ECS.Contracts;
using Common.ECS.Nodes;

namespace Common.ECS.Systems
{
    public class NetworkSystem : ISystem
    {
        private List<NetworkInputNode> NetworkInputNodes;
        private List<NetworkSyncNode> NetworkSyncNodes;

        public NetworkSystem()
        {
            NetworkInputNodes = new List<NetworkInputNode>();
            NetworkSyncNodes = new List<NetworkSyncNode>();
        }
        
        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}