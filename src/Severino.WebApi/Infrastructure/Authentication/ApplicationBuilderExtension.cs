namespace Severino.WebApi.Infrastructure.Authentication
{
    using Microsoft.AspNetCore.Builder;

    internal static class ApplicationBuilderExtension
    {
        internal static IApplicationBuilder UseSts(this IApplicationBuilder @this) => @this
            .UseIdentityServer();
    }
}
