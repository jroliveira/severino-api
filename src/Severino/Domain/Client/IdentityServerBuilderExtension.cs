namespace Severino.Domain.Client
{
    using Microsoft.Extensions.DependencyInjection;

    using Severino.Domain.Client.Data.Mongo;

    public static class IdentityServerBuilderExtension
    {
        public static IIdentityServerBuilder AddClient(this IIdentityServerBuilder @this) => @this
            .AddClientWithMongo();
    }
}
