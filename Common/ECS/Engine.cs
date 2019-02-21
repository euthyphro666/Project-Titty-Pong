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

        private List<ISystem> UpdateSystems { get; set; }
        private List<ISystem> RenderSystems { get; set; }
        private List<Entity> Entities { get; set; }

        public Engine()
        {
            UpdateSystems = new List<ISystem>();
            RenderSystems = new List<ISystem>();
            Entities = new List<Entity>();
        }

        public Engine AddSystem(ISystem system, bool shouldRender)
        {
            var systems = (shouldRender ? RenderSystems : UpdateSystems);
            if (systems.TrueForAll(s => s.GetType() != system.GetType()))
                systems.Add(system);
            return this;
        }

        public Engine AddEntity(Entity entity)
        {
            Entities.Add(entity);
            //Raise entities modified event
            return this;
        }

        public void Update()
        {
            foreach(var system in UpdateSystems)
            {
                system.Update();
            }
        }

        public void Render()
        {
            foreach (var system in RenderSystems)
            {
                system.Update();
            }
        }


    }
}
