using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TittyPong.IO
{
    public class InputMouseEventArgs : EventArgs
    {
        public float X;
        public float Y;

        public InputMouseEventArgs(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
