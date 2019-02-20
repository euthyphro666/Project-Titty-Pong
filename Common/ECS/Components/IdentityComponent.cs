namespace Common.ECS.Components
{
    public class IdentityComponent
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