namespace Severino.Domain
{
    using Microsoft.Extensions.DependencyInjection;

    using Severino.Domain.Client;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureDomain(this IServiceCollection @this) => @this
            .ConfigureClient();
    }
}
