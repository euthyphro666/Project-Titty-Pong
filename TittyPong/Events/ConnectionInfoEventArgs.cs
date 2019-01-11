using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TittyPong.Events
{
    public class ConnectionInfoEventArgs : EventArgs
    {
        public string DisplayName { get; set; }
        public string Address { get; set; }
        public ConnectionInfoEventArgs(string display, string address)
        {
            DisplayName = display;
            Address = address;
        }
    }
}
