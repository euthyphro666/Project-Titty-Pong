using Common.ECS.Contracts;

namespace Common.ECS.Components
{
    public class CollisionComponent: IComponent
    {
        public IdentityComponent Self { get; set; }
        public IdentityComponent Collider { get; set; }
    }
}