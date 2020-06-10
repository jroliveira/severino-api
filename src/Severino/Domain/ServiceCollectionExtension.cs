namespace Severino.Domain
{
    using Microsoft.Extensions.DependencyInjection;

    using Severino.Domain.Client;
    using Severino.Domain.Resource;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureDomain(this IServiceCollection @this) => @this
            .ConfigureClient()
            .ConfigureResource();
    }
}
