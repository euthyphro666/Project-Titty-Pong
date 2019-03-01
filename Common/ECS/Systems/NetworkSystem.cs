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

namespace Common.ECS.Systems
{
    public class NetworkSystem : ISystem
    {
        public uint Priority { get; set; }
        private readonly ISystemContext SystemContext;
        private readonly INetworkSocket Socket;
        private readonly List<NetworkInputNode> NetworkInputNodes;

        private bool IsServer;
        private GameSnapshot LastSnapshot;

        public NetworkSystem(ISystemContext systemContext, INetworkSocket socket)
        {
            SystemContext = systemContext;
            Socket = socket;

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
                Socket.Send(Serialize(MessageIds.GameSnapshot, LastSnapshot));
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


        private void ReceivedNetworkMessage(byte[] data)
        {
            // parse data to whatever type we need and handle it
            using (var stream = new MemoryStream(data))
            {
                var msgId = (MessageIds) stream.ReadByte();
                switch (msgId)
                {
                    case MessageIds.Invalid:
                        break;
                    case MessageIds.GameSnapshot:
                        var serializer = new BinaryFormatter();
                        if (serializer.Deserialize(stream) is GameSnapshot snapshot)
                            Debug.WriteLine("Received Snapshot: Input: " + snapshot.Input + " SnapshotNodes count: " + snapshot.SnapshotNodes.Count);
                        break;
                    default:
                        Debug.WriteLine("Unexpected message ID: " + msgId);
                        break;
                }
            }
        }

        private void OnGameSnapshotEvent(object sender, GameSnapshotEventArgs e)
        {
            LastSnapshot = e.Snapshot;
        }

        private void OnInputEvent(object sender, InputEventArgs e)
        {
            NetworkInputNodes.Add(new NetworkInputNode() {Player = e.Player, FrameInput = e.Input, FrameNumber = e.Frame});
        }

        private byte[] Serialize(MessageIds id, object objToSerialize)
        {
            try
            {
                using (var stream = new MemoryStream())
                {
                    stream.WriteByte((byte) id);
                    var serializer = new BinaryFormatter();
                    serializer.Serialize(stream, objToSerialize);

                    return stream.ToArray();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}