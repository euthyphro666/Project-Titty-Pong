using Common.ECS.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ECS.SystemEvents
{
    public class CollisionEventArgs : EventArgs
    {
        public CollisionNode Node1 { get; set; }
        public CollisionNode Node2 { get; set; }
    }
}
