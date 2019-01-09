using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TittyPong.Events
{
    public class StringEventArgs : EventArgs
    {
        public string Data;
        public StringEventArgs(string data)
        {
            Data = data;
        }
    }
}
