using Common.ECS.Contracts;
using Common.ECS.SystemEvents;

namespace Common.ECS
{
    public class SystemContext : ISystemContext
    {
        public IEventManager Events { get; }
        public float ViewportHeight { get; }
        public float ViewportWidth { get; }

        public SystemContext(float viewportWidth, float viewportHeight)
        {
            Events = new EventManager();
            ViewportHeight = viewportHeight;
            ViewportWidth = viewportWidth;
        }
        
        
    }
}