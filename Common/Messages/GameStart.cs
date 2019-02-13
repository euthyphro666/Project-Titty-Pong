namespace Common.Messages
{
    public class GameStart
    {
        public static RoomMessageIds MessageId => RoomMessageIds.GameStart;
        public long CurrentServerTick { get; set; }
    }
}