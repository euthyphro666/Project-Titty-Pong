using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Common.ECS.Contracts;
using Common.ECS.Nodes;
using Common.ECS.SystemEvents;
using Common.Networking;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;

namespace Common.ECS.Systems
{
    public class NetworkSystem : ISystem
    {
        public uint Priority { get; set; }
        private readonly ISystemContext SystemContext;
        private readonly INetworkSocket Socket;
        private readonly List<NetworkInputNode> NetworkInputNodes;

        private bool IsServer;
        private NetworkSnapshot LastSnapshot;

        private const float DivergenceLimit = 1.0f;

        public NetworkSystem(ISystemContext systemContext, INetworkSocket socket)
        {
            SystemContext = systemContext;
            Socket = socket;

            LastSnapshot = new NetworkSnapshot();

            IsServer = Socket is NetworkServer;

            NetworkInputNodes = new List<NetworkInputNode>();

            socket.Start(ReceivedNetworkMessage);

            if (IsServer)
            {
                SystemContext.Events.GameSnapshotEvent += OnGameSnapshotEvent;
            }
            else
            {
                SystemContext.Events.InputEvent += OnInputEvent;
                socket.Connect("192.168.1.223"); // TODO make this not hardcoded
            }
        }

        public void Update()
        {
            if (IsServer)
            {
                
                Socket.Send(MessageIds.GameSnapshot, LastSnapshot.ToByteArray());
            }
            else
            {
                // Send the input nodes
                if (NetworkInputNodes.Any())
                {
                    //Socket.Send();
                }
            }
        }


        private void ReceivedNetworkMessage(MessageIds msgId, byte[] data)
        {
            switch (msgId)
            {
                case MessageIds.Invalid:
                    break;
                case MessageIds.GameSnapshot:
                    var state = NetworkSnapshot.Parser.ParseFrom(data);
                    if (state != null)
                    {
                        Debug.WriteLine($"Received Snapshot: {state.FrameNumber} Entities: {state.Entities.Count} Timestamp: {state.LastUpdated}");
                        SystemContext.Events.RaiseNetworkSyncEvent(state, DivergenceLimit);
                    }

                    break;
                case MessageIds.PlayerInput:
                    var inputMsg = NetworkSnapshot.Parser.ParseFrom(data);
                    if (inputMsg != null)
                    {
                        Debug.WriteLine($"Received Input Message: ");
                    }
                    break;
                default:
                    Debug.WriteLine("Unexpected message ID: " + msgId);
                    break;
            }
        }

        private void OnGameSnapshotEvent(object sender, GameSnapshotEventArgs e)
        {
            var snap = new NetworkSnapshot() {FrameNumber = e.Snapshot.FrameNumber, LastUpdated = Timestamp.FromDateTime(DateTime.UtcNow)};

            foreach (var node in e.Snapshot.SnapshotNodes)
            {
                snap.Entities.Add(new NetworkSnapshot.Types.Entity()
                {
                    NetworkId = node.NetworkIdentity.Id,
                    PosX = node.Position.X,
                    PosY = node.Position.Y,
                    VelX = node.Velocity.X,
                    VelY = node.Velocity.Y
                });
            }

            LastSnapshot = snap;
        }

        private void OnInputEvent(object sender, InputEventArgs e)
        {
            NetworkInputNodes.Add(new NetworkInputNode() {Player = e.Player, FrameInput = e.Input, FrameNumber = e.Frame});
        }
    }
}