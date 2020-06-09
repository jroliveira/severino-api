namespace Severino.Domain.Client
{
    using Microsoft.Extensions.DependencyInjection;

    using Severino.Domain.Client.Data.Mongo;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureClient(this IServiceCollection @this) => @this
            .ConfigureClientWithMongo();
    }
}
