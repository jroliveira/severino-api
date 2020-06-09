namespace Severino.Domain.Client.Data.Mongo
{
    using Microsoft.Extensions.DependencyInjection;

    using Severino.Domain.Client.Data.Mongo.Stores;

    internal static class IdentityServerBuilderExtension
    {
        internal static IIdentityServerBuilder AddClientWithMongo(this IIdentityServerBuilder @this) => @this
            .AddClientStore<ClientStore>();
    }
}
