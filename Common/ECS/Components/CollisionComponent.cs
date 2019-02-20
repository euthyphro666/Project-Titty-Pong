namespace Common.ECS.Components
{
    public class CollisionComponent
    {
        public IdentityComponent Self { get; set; }
        public IdentityComponent Collider { get; set; }
    }
}