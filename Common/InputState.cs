using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class InputState
    {
        public enum Direction
        {
            None,
            Up,
            Down,
            Left,
            Right
        }

        public Direction State { get; set; }
        
    }
}
