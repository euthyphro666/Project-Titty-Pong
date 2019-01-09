using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TittyPong.Events
{
    public class IntEventArgs : EventArgs
    {
        public int Data;
        public IntEventArgs(int data)
        {
            Data = data;
        }
    }
}
