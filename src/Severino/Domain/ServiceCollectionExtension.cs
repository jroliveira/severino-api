namespace Severino.Domain
{
    using Microsoft.Extensions.DependencyInjection;

    using Severino.Domain.Client;
    using Severino.Domain.Resource;
    using Severino.Domain.User;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureDomain(this IServiceCollection @this) => @this
            .ConfigureClient()
            .ConfigureResource()
            .ConfigureUser();
    }
}
