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

        public event EventHandler<ComponentModifiedEventArgs> ComponentModifiedEvent;
        
        public Entity()
        {
            Components = new Dictionary<Type, IComponent>();
            Add(new IdentityComponent());
        }

        public Entity Add(IComponent component)
        {
            Components.Add(component.GetType(), component);
            ComponentModifiedEvent?.Invoke(this, new ComponentModifiedEventArgs(component.GetType()));
            return this;
        }

        public void Remove(Type componentType)
        {
            Components.Remove(componentType);
            ComponentModifiedEvent?.Invoke(this, new ComponentModifiedEventArgs(componentType));
        }

        public bool TryGetComponent(Type componentType, out IComponent component)
        {
            return Components.TryGetValue(componentType, out component);
        }
    }

    public class ComponentModifiedEventArgs : EventArgs
    {
        public Type ChangedComponentType { get; set; }
        
        public ComponentModifiedEventArgs(Type changedComponentType)
        {
            ChangedComponentType = changedComponentType;
        }

    }
}