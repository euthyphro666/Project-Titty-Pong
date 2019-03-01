using Common.ECS.Contracts;

namespace Common.ECS.Components
{
    public class IdentityComponent : IComponent
    {
        private static int CurrentId = 0;
        private static int NextAvailableId => CurrentId++;
        
        public int Id { get; }
        public string Name { get; }

        public IdentityComponent(string name)
        {
            Name = name;
            Id = NextAvailableId;
        }
    }
}