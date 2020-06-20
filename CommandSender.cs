using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Coyote;
using Microsoft.Coyote.Actors;

namespace Coyote.Commands
{
    public class IncrementCountCommand : Event
    {
        public string Message;

        public IncrementCountCommand(string message) { this.Message = message; }
    }

    //[OnEventDoAction(typeof(IncrementCountCommand), nameof(HandleIncrementCountCommand))]
    public class CommandSender : NetworkMonitor
    {
        private HttpClient Http;


        protected override Task OnInitializeAsync(Event initialEvent)
        {
            Console.WriteLine($"In OnInitializeAsync of command sender");
            Commands = new List<Event>();
            return base.OnInitializeAsync(initialEvent);
        }
        
        public async Task HandleIncrementCountCommand(IncrementCountCommand command)
        {
            Console.WriteLine($"Got here HandleIncrementCountCommand of command sender");

            if (Connected)
            {
                //send command
                try
                {
                    using (var Http = new HttpClient())
                    {
                        Console.WriteLine($"trying to post a command");
                        var postedOK = await Http.PostJsonAsync<Coyote.Commands.IncrementCountCommand>("api/values", command);

                    }
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    var errorCode = ex.HResult;
                }
            }
            else
            {
                Commands.Add(command);
            }
        }

    }
}
