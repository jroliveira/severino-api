namespace Severino.Domain
{
    using Microsoft.Extensions.DependencyInjection;

    using Severino.Domain.Client;
    using Severino.Domain.Resource;

    public static class IdentityServerBuilderExtension
    {
        public static IIdentityServerBuilder AddDomain(this IIdentityServerBuilder @this) => @this
            .AddClient()
            .AddResource();
    }
}
