using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
    public class InputState
    {
        public Direction State { get; set; }
        public long Timestamp { get; set; }
    }
}
