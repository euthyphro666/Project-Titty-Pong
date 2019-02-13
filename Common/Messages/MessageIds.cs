namespace Common.Messages
{
    public enum CommunicationMessageIds : byte
    {
        Invalid,
        ConnectionRequest,
        ConnectionResponse,
        StartGameRequest,
        StartGameResponse,
        StartGameRefused,
        JoinRoomRequest,
        RoomMessage
    }

    public enum RoomMessageIds : byte
    {
        Invalid,
        RoomConfirmation,
        GameStart,
        GameInputUpdate,
        Update,
    }
}