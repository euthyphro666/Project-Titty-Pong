using System;
using Lidgren.Network;

namespace TittyPong.Events
{
    public class ByteArrayEventArgs : EventArgs
    {
        public NetDeliveryMethod DeliveryMethod { get; set; }
        public byte[] Data { get; set; }

        public ByteArrayEventArgs()
        {
            Data = new byte[]{};
            DeliveryMethod = NetDeliveryMethod.Unreliable;
        }
        
        public ByteArrayEventArgs(byte[] data, NetDeliveryMethod method = NetDeliveryMethod.Unreliable)
        {
            Data = data;
        }
    }
}