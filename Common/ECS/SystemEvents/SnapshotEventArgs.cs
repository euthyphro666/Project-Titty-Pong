using Common.ECS.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ECS.SystemEvents
{
    public class SnapshotEventArgs : EventArgs
    {
        public List<DynamicSnapshotNode> Snapshot { get; set; }
    }
}
