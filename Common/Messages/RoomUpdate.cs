using Common.Game_Data;

namespace Common.Messages
{
    public class RoomUpdate
    {
        public static RoomMessageIds MessageId => RoomMessageIds.Update;
        public GameState State { get; set; }
    }
}