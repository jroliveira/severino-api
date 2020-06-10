namespace Severino.Domain.Resource
{
    using Microsoft.Extensions.DependencyInjection;

    using Severino.Domain.Resource.Data.Mongo;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureResource(this IServiceCollection @this) => @this
            .ConfigureResourceWithMongo();
    }
}
