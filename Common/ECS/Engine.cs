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
            Systems = new List<ISystem>();
            Entities = new List<Entity>();
        }

        public void AddSystem(ISystem system)
        {
            if(Systems.TrueForAll(s => s.GetType() != system.GetType()))
                Systems.Add(system);
        }

        public void Update()
        {
            foreach(var system in Systems)
            {
                system.Update();
            }
        }
        

    }
}
