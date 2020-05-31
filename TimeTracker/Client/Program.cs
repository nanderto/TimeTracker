using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace TimeTracker.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Got here XXXXX");
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddBaseAddressHttpClient();
            builder.Services.AddSingleton<CoyoteRuntime, CoyoteRuntime>();
            await builder.Build().RunAsync();
        }
    }
}
