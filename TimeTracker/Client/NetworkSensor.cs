using Microsoft.Coyote;
using Microsoft.Coyote.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static TimeTracker.Client.ConnectionMonitor;
using System.Net.Http;
using TimeTracker.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.NetworkInformation;

namespace TimeTracker.Client
{
    [OnEventDoAction(typeof(TimerElapsedEvent), nameof(HandleTimeout))]
    [OnEventDoAction(typeof(RegisterClientEvent), nameof(OnRegisterClient))]
    internal class NetworkSensor : Actor
    {
        private bool NetworkConnected;
        private ActorId Client;
        private List<ActorId> Clients = new List<ActorId>();
        private HttpClient Http;
        private bool uninitialized;

        protected override Task OnInitializeAsync(Event initialEvent)
        {
            Console.WriteLine($"In OnInitializeAsync");
            this.StartPeriodicTimer(TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(1000));
            //this.NetworkConnected = this.PingServer().Result;
            //if (this.NetworkConnected)
            //{
            //    //do something?
            //    //this.Monitor<DoorSafetyMonitor>(new DoorOpenEvent(this.NetworkConnected));
            //    foreach (var item in this.Clients)
            //    {
            //        //tell them it is connected
            //    }
            //}

            return base.OnInitializeAsync(initialEvent);
        }

        private async Task HandleTimeout()
        {
            var connected = await PingServer();
            if (this.NetworkConnected != connected)
            {
                if(connected)
                {
                    OnNetworkConnected();
                }
                else 
                {
                    OnNetworkDisConnected();
                }

                this.NetworkConnected = connected;
            }
        }

        private async Task<bool> PingServer()
        {
            bool connected = false;
            string message = string.Empty;
            int errorCode = -1;

            try
            {
                using (var Http = new HttpClient())
                {

                    var connectedOK = await Http.GetJsonAsync<OK>("api/Ping");
                    if (connectedOK.OKProperty == "OK")
                    {
                        connected = true;
                    }
                    else
                    {
                        connected = false;
                    }
                }
            }
            catch (Exception ex)
            {
                connected = false;
                message = ex.Message;
                errorCode = ex.HResult;
            }

            return connected;
        }

        private void OnRegisterClient(Event e)
        {
            Console.WriteLine($"In OnRegisterClient");
            var client = ((RegisterClientEvent)e).Caller;
            this.Clients.Add(client);
        }

        private void OnNetworkConnected()
        {
            foreach (var item in this.Clients)
            {
                this.SendEvent(item, new ConnectedEvent(this.NetworkConnected));
            }

        }

        private void OnNetworkDisConnected()
        {
            foreach (var item in this.Clients)
            {
                this.SendEvent(item, new DisConnectedEvent(this.NetworkConnected));
            }
        }
    }
}
