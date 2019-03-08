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
        private NetworkSnapshots GameState;

        public NetworkSystem(ISystemContext systemContext, INetworkSocket socket)
        {
            SystemContext = systemContext;
            Socket = socket;

            GameState = new NetworkSnapshots();

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
                
                Socket.Send(MessageIds.GameSnapshot, GameState.ToByteArray());
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
                    var state = NetworkSnapshots.Parser.ParseFrom(data);
                    if (state != null)
                    {
                        Debug.WriteLine("Received Snapshot: SnapshotNodes count: " + state.Snapshots.Count);
                    }

                    break;
                default:
                    Debug.WriteLine("Unexpected message ID: " + msgId);
                    break;
            }
        }

        private void OnGameSnapshotEvent(object sender, GameSnapshotEventArgs e)
        {
            var snap = new NetworkSnapshot() {FrameNumber = e.Snapshot.FrameNumber, LastUpdated = new Timestamp()};

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


            if(GameState.Snapshots.Count > 255)
                GameState.Snapshots.RemoveAt(0);
            
            GameState.Snapshots.Add(snap);
            
            Debug.WriteLine("Generating snapshot: " + snap.FrameNumber);
        }

        private void OnInputEvent(object sender, InputEventArgs e)
        {
            NetworkInputNodes.Add(new NetworkInputNode() {Player = e.Player, FrameInput = e.Input, FrameNumber = e.Frame});
        }
    }
}