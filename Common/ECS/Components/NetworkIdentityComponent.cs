using System;
using Common.ECS.Contracts;

namespace Common.ECS.Components
{
    
    public class NetworkIdentityComponent : IComponent
    {
        private static int CurrentId = 1;
        private static int NextAvailableId => CurrentId++;
        
        public int Id { get; }
        public string Name { get; }

        public NetworkIdentityComponent(string name)
        {
            Name = name;
            Id = NextAvailableId;
        }
    }
}