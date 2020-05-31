using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracker.Client
{
    using Microsoft.Coyote.Specifications;
    using Microsoft.Coyote;

    internal class ConnectionMonitor : Monitor
    {
        //public class ConnectedEvent : Event { }

        //public class DisConectedEvent : Event { }

        [Start]
        [Cold]
        [OnEventGotoState(typeof(ConnectedEvent), typeof(Connected))]
        [IgnoreEvents(typeof(DisConnectedEvent))]
        private class DisConnected : State { }

        [Hot]
        [OnEventGotoState(typeof(DisConnectedEvent), typeof(DisConnected))]
        [IgnoreEvents(typeof(ConnectedEvent))]
        private class Connected : State { }
    }
}