using System.Collections.Generic;

namespace Common.Messages
{
    public class ConnectionResponse
    {
        public static CommunicationMessageIds MessageId => CommunicationMessageIds.ConnectionResponse;
        public Dictionary<string, string> AvailableClients { get; set; }
    }
}