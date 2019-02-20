using System.Collections.Generic;
using Common.Utils;

namespace Common.ECS
{
    public class Entity
    {
        public int Id { get; private set; }

        private readonly Dictionary<string, object> Components;

        public Entity()
        {
            Id = EntityIdManager.NextAvailableId;
            Components = new Dictionary<string, object>();
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