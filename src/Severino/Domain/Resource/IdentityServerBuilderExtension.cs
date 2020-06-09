namespace Severino.Domain.Resource
{
    using Microsoft.Extensions.DependencyInjection;

    using Severino.Domain.Resource.Data.Mongo;

    public static class IdentityServerBuilderExtension
    {
        public static IIdentityServerBuilder AddResource(this IIdentityServerBuilder @this) => @this
            .AddResourceWithMongo();
    }
}
