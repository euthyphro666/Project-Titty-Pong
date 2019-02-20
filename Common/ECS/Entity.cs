using System;
using System.Collections.Generic;
using Common.ECS.Components;
using Common.ECS.Contracts;
using Common.Utils;

namespace Common.ECS
{
    public class Entity
    {

        private readonly Dictionary<Type, IComponent> Components;

        public Entity()
        {
            Components = new Dictionary<Type, IComponent>();
            Add(new IdentityComponent());
        }

        public void Add(IComponent component)
        {
            Components.Add(component.GetType(), component);
        }

        public void Remove(Type componentType)
        {
            Components.Remove(componentType);
        }

        public IComponent Get(Type componentType)
        {
            return Components[componentType];
        }
    }
}