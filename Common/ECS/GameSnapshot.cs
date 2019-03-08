using System;
using System.Collections.Generic;
using Common.ECS.Nodes;

namespace Common.ECS
{
    public class GameSnapshot
    {
        public int FrameNumber { get; set; }
        public List<DynamicSnapshotNode> SnapshotNodes { get; set; }
        public byte Input { get; set; }

        public GameSnapshot(int frameNumber, List<DynamicSnapshotNode> snapshotNodes, byte input)
        {
            FrameNumber = frameNumber;
            SnapshotNodes = snapshotNodes;
            Input = input;
        }

    }
}