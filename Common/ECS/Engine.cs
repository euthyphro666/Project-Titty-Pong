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
        private SortedList<uint, ISystem> UpdateSystems { get; set; }
        private SortedList<uint, ISystem> RenderSystems { get; set; }
        private List<Entity> Entities { get; set; }
        private ISystemContext Context;

        public Engine(ISystemContext context)
        {
            Context = context;
            UpdateSystems = new SortedList<uint, ISystem>();
            RenderSystems = new SortedList<uint, ISystem>();
            Entities = new List<Entity>();
        }

        public Engine AddSystem(ISystem system, uint priority, bool shouldRender)
        {
            system.Priority = priority;
            var systems = (shouldRender ? RenderSystems : UpdateSystems);
            if (systems.Values.ToList().TrueForAll(s => s.GetType() != system.GetType()))
                systems.Add(priority, system);
            return this;
        }

        public Engine AddEntity(Entity entity)
        {
            Entities.Add(entity);
            Context.Events.RaiseEntityAddedEvent(entity);
            return this;
        }

        public void Update()
        {
            foreach(var system in UpdateSystems.Values)
            {
                system.Update();
            }
        }

        public void Render()
        {
            foreach (var system in RenderSystems.Values)
            {
                system.Update();
            }
        }


    }
}
