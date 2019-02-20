using Common.ECS.Contracts;
using Common.ECS.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ECS.Systems
{
    public class RenderSystem : ISystem
    {
        public List<RenderNode> Targets { get; set; }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
