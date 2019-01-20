using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TittyPong.Events
{
    public class ObjectEventArgs : EventArgs
    {
        public object Data;
        public ObjectEventArgs(object data)
        {
            Data = data;
        }
    }
}
