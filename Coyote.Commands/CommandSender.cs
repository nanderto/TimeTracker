using System;

namespace Coyote.Commands
{
    [OnEventDoAction(typeof(StartupEvent), nameof(HandleStartUpMessage))]
    [OnEventDoAction(typeof(StartupReplyEvent), nameof(HandleStartUpRepyMessage))]
    public class CommandSender : Actor
    {
        private void HandleStartUpMessage(Event e)
        {
            Console.WriteLine("Got here HandleStartUpMessage zzzz");

            if (e is StartupEvent se)
            {
                Console.WriteLine($"Got here startupevent is a startup event {se.Caller?.ToString()}");
                this.SendEvent(se.Caller, new StartupReplyEvent(this.Id));
            }
        }

        private void HandleStartUpRepyMessage(Event e)
        {
            Console.WriteLine("Got here HandleStartUpMessage zzzz");

            if (e is StartupReplyEvent se)
            {
                Console.WriteLine($"Got here startupreplyevent is a startup event {se.Caller?.ToString()}");
            }
        }
    }
}
