using Microsoft.Coyote;
using Microsoft.Coyote.Actors;
using System;

namespace TimeTracker.Client
{
    internal class StartupEvent : Event
    {
        private readonly bool startup;

        public readonly ActorId Caller;
        private readonly ActorId replyTo;

        public StartupEvent(ActorId caller)
        {
            Console.WriteLine($"Got here StartupEvent constructor {caller.ToString()}");
            Caller = caller;
        }

        public StartupEvent(ActorId caller, ActorId replyTo)
        {
            Console.WriteLine($"Got here StartupEvent constructor {caller.ToString()}");
            Caller = caller;
            this.replyTo = replyTo;
        }

        public StartupEvent(bool startup)
        {
            Console.WriteLine("Got here StartupEvent constructor zazaza");
            this.startup = startup;
        }
    }

    internal class StartupReplyEvent : Event
    {
        private readonly bool startup;

        public readonly ActorId Caller;

        public StartupReplyEvent(ActorId caller)
        {
            Caller = caller;
        }

        public StartupReplyEvent(bool startup)
        {
            this.startup = startup;
        }
    }
}