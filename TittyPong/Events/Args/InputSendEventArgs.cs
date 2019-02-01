using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TittyPong.Events.Args
{
    public class InputSendEventArgs : EventArgs
    {
        public List<InputState> States { get; set; }
    }
}
