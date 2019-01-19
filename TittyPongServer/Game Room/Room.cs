using System;

namespace TittyPongServer.Game_Room
{
    public class Room
    {
        public Guid RoomId;

        public Room()
        {
            RoomId = Guid.NewGuid();
        }

        public Guid GetRoomId()
        {
            return RoomId;
        }
    }
}