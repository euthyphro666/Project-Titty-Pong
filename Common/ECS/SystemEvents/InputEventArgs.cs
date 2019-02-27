using Common.ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ECS.SystemEvents
{
    public class InputEventArgs : EventArgs
    {
        public PlayerNumber Player { get; set; }
        public byte Input { get; set; }
    }
}
