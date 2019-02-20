using System.Collections.Generic;
using Common.ECS.Components;
using Common.Utils;

namespace Common.ECS
{
    public class Entity
    {

        private readonly Dictionary<string, object> Components;

        public Entity()
        {
            Components = new Dictionary<string, object>();
            Add(new IdentityComponent());
        }

        public void Add(object component)
        {
            Components.Add(component.ToString(), component);
        }

        public void Remove(object component)
        {
            Components.Remove(component.ToString());
        }

        public object Get(string componentType)
        {
            return Components[componentType];
        }
    }
}