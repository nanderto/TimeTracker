using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coyote.Commands
{
    using Microsoft.Coyote;
    using Microsoft.Coyote.Actors;
    using System.Net.Http;

    internal class DisConnectedEvent : Event
    {
        private bool networkConnected;

        public DisConnectedEvent(bool networkConnected)
        {
            this.networkConnected = networkConnected;
        }
    }

    internal class ConnectedEvent : Event
    {
        private bool networkConnected;

        public ConnectedEvent(bool networkConnected)
        {
            this.networkConnected = networkConnected;
        }
    }

    /// <summary>
    /// Pass this caller ActorId to each sensor so it knows how to call you back.
    /// </summary>
    public class RegisterClientEvent : Event
    {
        public ActorId Caller;

        public RegisterClientEvent(ActorId caller) { this.Caller = caller; }
    }

    public class NetworkSensorInitialEvent : Event
    {
        public HttpClient httpClient;

        public NetworkSensorInitialEvent(HttpClient httpClient) { this.httpClient = httpClient; }
    }

    public class InjectHttpClientInitialEvent : Event
    {
        public HttpClient httpClient;

        public InjectHttpClientInitialEvent(HttpClient httpClient) { this.httpClient = httpClient; }
    }
}
