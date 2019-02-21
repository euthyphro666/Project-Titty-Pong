namespace Common.ECS.Contracts
{
    public interface ISystemContext
    {
        IEventManager Events { get; }
        float ViewportHeight { get; }
        float ViewportWidth { get; }
    }
}