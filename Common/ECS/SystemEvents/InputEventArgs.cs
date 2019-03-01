using Common.ECS.Components;
using Common.IO;
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
        public Input Input { get; set; }
    }
}
