using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ECS.SystemEvents
{
    public class EntityAddedEventArgs : EventArgs
    {
        public Entity Target { get; set; }
    }
}
