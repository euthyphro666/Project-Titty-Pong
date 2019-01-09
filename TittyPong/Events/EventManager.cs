using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TittyPong.Events
{
    public class EventManager
    {
        public event EventHandler<StringEventArgs> ConnectionInfoEvent;
        public void OnConnectionInfoEvent(object sender, StringEventArgs e)
        {
            ConnectionInfoEvent?.Invoke(sender, e);
        }

    }
}
