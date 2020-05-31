using Microsoft.Coyote.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracker.Client
{
    internal class NetworkMonitor : Monitor
    {
        [Start]
        [OnEventGotoState(typeof(DisConnectedEvent), typeof(Error))]
        [IgnoreEvents(typeof(ConnectedEvent))]
        private class Init : State { }

        [OnEventDoAction(typeof(ConnectedEvent), nameof(OnConnected))]
        private class Error : State { }

        private void OnConnected()
        {
            Console.WriteLine(" Network Monitor OnConnected");
            //"Send messages"

        }
    }
}
