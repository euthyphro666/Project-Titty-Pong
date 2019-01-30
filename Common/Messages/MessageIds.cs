namespace Common.Messages
{
    public enum CommunicationMessageIds
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

    public enum RoomMessageIds
    {
        Invalid,
        RoomConfirmation,
        GameInputUpdate,
    }
}