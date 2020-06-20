using Microsoft.Coyote;
using Microsoft.Coyote.Actors;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Coyote.Commands
{
    [OnEventDoAction(typeof(ConnectedEvent), nameof(OnNetworkConnected))]
    [OnEventDoAction(typeof(DisConnectedEvent), nameof(OnNetworkDisConnected))]
    public class NetworkMonitor : Actor
    {

        private List<Event> commands;

        private bool connected;

        public bool Connected { get => connected; set => connected = value; }

        public List<Event> Commands { get => commands; set => commands = value; }

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
                    using (var Http = new HttpClient())
                    {
                        foreach (var item in Commands)
                        {
                            Console.WriteLine($"Trying to post in network monitor a command from the list");
                            var postedOK = await Http.PostJsonAsync<Coyote.Commands.IncrementCountCommand>("api/values", item);
                        }

                    }
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
