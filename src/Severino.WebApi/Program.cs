namespace Severino.WebApi
{
    using Microsoft.AspNetCore.Hosting;

    using Severino.WebApi.Infrastructure.Api;
    using Severino.WebApi.Infrastructure.Logging;
    using Severino.WebApi.Infrastructure.Metric;

    internal class Program
    {
        public static void Main(string[] args) => new WebHostBuilder()
            .ConfigureApi()
            .ConfigureLogging()
            .ConfigureMetric()
            .UseStartup<Startup>()
            .Build()
            .Run();
    }
}
