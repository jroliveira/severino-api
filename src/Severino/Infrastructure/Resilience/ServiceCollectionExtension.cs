namespace Severino.Infrastructure.Resilience
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Severino.Infrastructure.Resilience.Configurations;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureResilience(this IServiceCollection @this, IConfiguration configuration) => @this
            .Configure<ResilienceConfiguration>(configuration.GetSection("resilience"))
            .AddSingleton<ResiliencePolicy>();
    }
}
