namespace TittyPong.Events
{
    public class StartGameResponseEventArgs
    {
        public string RequestingClientMac { get; set; }
        public string RespondingClientMac { get; set; }
        public bool StartGameAccepted { get; set; }
    }
}