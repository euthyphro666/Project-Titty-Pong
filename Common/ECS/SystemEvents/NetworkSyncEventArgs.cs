using System;
using Common.Networking;

namespace Common.ECS.SystemEvents
{
    public class NetworkSyncEventArgs : EventArgs
    {
        public NetworkSnapshot Snapshot { get; set; }
        public float DivergenceLimit { get; set; }

        public NetworkSyncEventArgs(NetworkSnapshot snapshot, float divergenceLimit)
        {
            DivergenceLimit = divergenceLimit;
            Snapshot = snapshot;
        }
    }
}