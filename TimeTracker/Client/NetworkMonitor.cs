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

namespace TimeTracker.Client
{

    [OnEventDoAction(typeof(ConnectedEvent), nameof(OnNetworkConnected))]
    [OnEventDoAction(typeof(DisConnectedEvent), nameof(OnNetworkDisConnected))]
    [OnEventDoAction(typeof(RegisterClientEvent), nameof(OnRegisterClient))]
    internal class NetworkSensor : Actor
    {
        private bool NetworkConnected;
        private ActorId Client;
        private HttpClient Http;

        protected override Task OnInitializeAsync(Event initialEvent)
        {
            this.NetworkConnected = this.PingServer().Result;
            if (this.NetworkConnected)
            {
                //do something?
                //this.Monitor<DoorSafetyMonitor>(new DoorOpenEvent(this.NetworkConnected));
            }

            return base.OnInitializeAsync(initialEvent);
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
            this.Client = ((RegisterClientEvent)e).Caller;
        }

        private void OnNetworkConnected()
        {
            if (this.Client != null)
            {
                this.SendEvent(this.Client, new ConnectedEvent(this.NetworkConnected));
            }
        }

        private void OnNetworkDisConnected()
        {
            if (this.Client != null)
            {
                this.SendEvent(this.Client, new DisConnectedEvent(this.NetworkConnected));
            }
        }
    }
}
