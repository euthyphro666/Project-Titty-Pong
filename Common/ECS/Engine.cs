using Common.ECS.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ECS
{
    public class Engine
    {

        private List<ISystem> Systems { get; set; }
        private List<Entity> Entities { get; set; }

        public Engine()
        {

        }



    }
}
