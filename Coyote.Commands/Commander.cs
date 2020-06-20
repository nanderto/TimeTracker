using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Coyote;
using Microsoft.Coyote.Actors;

namespace Coyote.Commands
{
    public class IncrementCountEvent : Event
    {
        public Command Command;

        public IncrementCountEvent(Command command) { this.Command = command; }
    }

    [OnEventDoAction(typeof(ConnectedEvent), nameof(OnNetworkConnected))]
    [OnEventDoAction(typeof(DisConnectedEvent), nameof(OnNetworkDisConnected))]
    [OnEventDoAction(typeof(IncrementCountEvent), nameof(HandleIncrementCountEvent))]
    public class Commander : Actor
    {
        private HttpClient Http;


        protected override Task OnInitializeAsync(Event initialEvent)
        {
            Http = ((InjectHttpClientInitialEvent)initialEvent).httpClient;
            Console.WriteLine($"In OnInitializeAsync of command sender");
            Commands = new List<Command>();
            return base.OnInitializeAsync(initialEvent);
        }
        
        public async Task HandleIncrementCountEvent(Event incrementCountEvent)
        {
            Console.WriteLine($"Got here HandleIncrementCountCommand of command sender");

            var command = ((IncrementCountEvent)incrementCountEvent).Command;
            if (Connected)
            {
                //send command
                try
                {
                    //using (var Http = new HttpClient())
                    //{
                        Console.WriteLine($"trying to post a command {command.ToString()}");
                        var response = await Http.PostAsJsonAsync<Coyote.Commands.Command>("api/values", command);
                        var content = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"posted a command: {content}");
                    if (response.IsSuccessStatusCode)
                    { }

                    //}
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Caught an exception while posting a command: {ex.Message}");
                    var message = ex.Message;
                    var errorCode = ex.HResult;
                }
            }
            else
            {
                Commands.Add(command);
            }
        }

        private List<Command> commands;

        private bool connected;

        public bool Connected { get => connected; set => connected = value; }

        public List<Command> Commands { get => commands; set => commands = value; }

        private async Task OnNetworkConnected()
        {
            Console.WriteLine($"Got here OnNetworkConnected of network monitor");

            Connected = true;
            await SendQueuedCommands();
        }

        private void OnNetworkDisConnected()
        {
            Console.WriteLine($"Got here OnNetworkDisConnected of network monitor");

            Connected = false;
        }

        private async Task SendQueuedCommands()
        {
            Console.WriteLine($"Got here SendQueuedCommands of network monitor");

            if (Connected)
            {
                //send command
                try
                {
                    //using (var Http = new HttpClient())
                    //{
                        foreach (var item in Commands)
                        {
                            Console.WriteLine($"Trying to post in network monitor a command from the list");
                            var postedOK = await Http.PostJsonAsync<Coyote.Commands.IncrementCountEvent>("api/values", item);
                        }

                    //}
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    var errorCode = ex.HResult;
                }
            }
        }
    }
}
