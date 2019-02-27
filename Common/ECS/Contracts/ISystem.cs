using Common.ECS.SystemEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ECS.Contracts
{
    public interface ISystem
    {
        uint Priority { get; set; }
        
        void Update();
    }
}
