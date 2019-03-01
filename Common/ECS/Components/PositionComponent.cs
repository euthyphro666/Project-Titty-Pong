using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ECS.Contracts;

namespace Common.ECS.Components
{
    [Serializable]
    public class PositionComponent : IComponent
    {
        public float X;
        public float Y;
    }
}
