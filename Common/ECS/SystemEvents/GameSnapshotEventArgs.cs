using System;

namespace Common.ECS.SystemEvents
{
    public class GameSnapshotEventArgs : EventArgs
    {
        public GameSnapshot Snapshot { get; set; }

        public GameSnapshotEventArgs(GameSnapshot snapshot)
        {
            Snapshot = snapshot;
        }
    }
}