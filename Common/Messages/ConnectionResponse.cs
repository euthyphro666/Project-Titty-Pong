using System.Collections.Generic;

namespace Common.Messages
{
    public class ConnectionResponse
    {
        public static MessageIds MessageId => MessageIds.ConnectionResponse;
        public Dictionary<string, string> AvailableClients { get; set; }
    }
}