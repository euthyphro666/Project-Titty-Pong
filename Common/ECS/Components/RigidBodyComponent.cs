using System.Linq.Expressions;

namespace Common.ECS.Components
{
    public class RigidBodyComponent
    {
        public bool IsRect { get; set; }
        public bool IsKinematic { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
    }
}