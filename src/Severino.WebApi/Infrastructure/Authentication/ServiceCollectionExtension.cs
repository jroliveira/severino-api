namespace Severino.WebApi.Infrastructure.Authentication
{
    using Microsoft.Extensions.DependencyInjection;

    using Severino.Domain;

    internal static class ServiceCollectionExtension
    {
        internal static IServiceCollection ConfigureSts(this IServiceCollection @this)
        {
            @this
                .AddIdentityServer()
                .AddCorsPolicyService<CorsPolicyService>()
                .AddDeveloperSigningCredential()
                .AddDomain();

            return @this;
        }
    }
}
