namespace TittyPong.Events
{
    public class ConnectEventArgs
    {
        public string Ip { get; set; }

        public ConnectEventArgs(string ip)
        {
            Ip = ip;
        }
    }
}