namespace Severino.Domain.Resource.Data.Mongo
{
    using Microsoft.Extensions.DependencyInjection;

    using Severino.Domain.Resource.Data.Mongo.Stores;

    internal static class IdentityServerBuilderExtension
    {
        internal static IIdentityServerBuilder AddResourceWithMongo(this IIdentityServerBuilder @this) => @this
            .AddResourceStore<ResourceStore>();
    }
}
