using Microsoft.Coyote.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracker.Client
{
    public class CoyoteRuntime
    {
        IActorRuntime runtime; // Configuration.Create().WithVerbosityEnabled());

        public IActorRuntime Runtime { get => runtime; set => runtime = value; }

        public CoyoteRuntime()
        {
            Runtime = RuntimeFactory.Create();
            Execute(Runtime);
            Console.WriteLine("Got here XZXZXZ");
        }

        private static void OnRuntimeFailure(Exception ex)
        {
            Console.WriteLine("Unhandled exception: {0}", ex.Message);
        }

        [Microsoft.Coyote.SystematicTesting.Test]
        public static void Execute(IActorRuntime runtime)
        {
            Console.WriteLine("Registering Monitor");
            runtime.OnFailure += OnRuntimeFailure;
            runtime.RegisterMonitor<NetworkMonitor>();
            //ActorId driver = runtime.CreateActor(typeof(FailoverDriver), new ConfigEvent(RunForever));
            //runtime.SendEvent(driver, new FailoverDriver.StartTestEvent());
        }

    }
}
