namespace Common.Messages
{
    public enum CommunicationMessageIds
    {
        ConnectionRequest,
        ConnectionResponse,
        StartGameRequest,
        StartGameResponse,
        JoinRoomRequest,
        RoomMessage
    }

    public enum RoomMessageIds
    {
        RoomConfirmation,
        GameInputUpdate,
    }
}