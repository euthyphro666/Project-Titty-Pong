using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TittyPong.Events.Args
{
    public class JoinRoomEventArgs : EventArgs
    {
        public string ClientAId { get; set; }
        public string ClientBId { get; set; }

        public string ClientADisplay { get; set; }
        public string ClientBDisplay { get; set; }

        public Guid RoomId { get; set; }
    }
}
