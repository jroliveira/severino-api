namespace Severino.Domain.User.Data.Mongo
{
    using Microsoft.Extensions.DependencyInjection;

    using Severino.Domain.User.Data.Mongo.Validators;

    internal static class IdentityServerBuilderExtension
    {
        internal static IIdentityServerBuilder AddUserWithMongo(this IIdentityServerBuilder @this) => @this
            .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();
    }
}
