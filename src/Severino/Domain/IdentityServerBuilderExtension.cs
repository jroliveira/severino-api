namespace Severino.Domain
{
    using Microsoft.Extensions.DependencyInjection;

    using Severino.Domain.Client;
    using Severino.Domain.Resource;
    using Severino.Domain.User;

    public static class IdentityServerBuilderExtension
    {
        public static IIdentityServerBuilder AddDomain(this IIdentityServerBuilder @this) => @this
            .AddClient()
            .AddResource()
            .AddUser();
    }
}
