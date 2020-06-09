namespace Severino.WebApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Severino.Domain;
    using Severino.Infrastructure.Data.Mongo;
    using Severino.Infrastructure.Resilience;
    using Severino.WebApi.Infrastructure.Api;
    using Severino.WebApi.Infrastructure.Authentication;
    using Severino.WebApi.Infrastructure.Caching;
    using Severino.WebApi.Infrastructure.ErrorHandling;
    using Severino.WebApi.Infrastructure.IpRateLimiting;
    using Severino.WebApi.Infrastructure.Metric;
    using Severino.WebApi.Infrastructure.Swagger;
    using Severino.WebApi.Infrastructure.Tracing;
    using Severino.WebApi.Infrastructure.Versioning;

    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) => services
            .ConfigureCache()
            .ConfigureIpRateLimiting(this.Configuration)
            .ConfigureResilience(this.Configuration)
            .ConfigureMongo(this.Configuration)
            .ConfigureApi()
            .ConfigureVersioning()
            .ConfigureMetric()
            .ConfigureSwagger(this.Configuration)
            .ConfigureTracing(this.Configuration)
            .ConfigureSts();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment) => app
            .UseErrorHandling(environment)
            .UseSts()
            .UseApi()
            .UseMetric()
            .UseSwagger(this.Configuration)
            .UseTracing(this.Configuration);
    }
}
