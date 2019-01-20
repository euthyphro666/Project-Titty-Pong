namespace Common.Messages
{
    public enum CommunicationMessageIds
    {
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
        RoomConfirmation,
        GameInputUpdate,
    }
}