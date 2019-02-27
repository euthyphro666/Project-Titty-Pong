using System.Linq.Expressions;
using Common.ECS.Contracts;

namespace Common.ECS.Components
{
    public class RigidBodyComponent: IComponent
    {
        public bool IsRect { get; set; }
        public bool IsDynamic { get; set; }
        
        public bool IsKinematic { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
    }
}