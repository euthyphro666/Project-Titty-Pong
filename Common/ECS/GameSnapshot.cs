using System;
using System.Collections.Generic;
using Common.ECS.Nodes;

namespace Common.ECS
{
    [Serializable]
    public class GameSnapshot
    {
        public List<DynamicSnapshotNode> SnapshotNodes { get; set; }
        public byte Input { get; set; }

        public GameSnapshot(List<DynamicSnapshotNode> snapshotNodes, byte input)
        {
            SnapshotNodes = snapshotNodes;
            Input = input;
        }

    }
}