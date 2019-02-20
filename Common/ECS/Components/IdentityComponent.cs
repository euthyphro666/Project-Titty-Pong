using Common.ECS.Contracts;

namespace Common.ECS.Components
{
    public class IdentityComponent : IComponent
    {
        private static int CurrentId = 0;
        private static int NextAvailableId => CurrentId++;
        
        public int Id { get; }

        public IdentityComponent()
        {
            Id = NextAvailableId;
        }
    }
}