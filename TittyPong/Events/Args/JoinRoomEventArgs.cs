using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TittyPong.Events.Args
{
    public class JoinRoomEventArgs : EventArgs
    {
        public Guid RoomId { get; set; }
    }
}
