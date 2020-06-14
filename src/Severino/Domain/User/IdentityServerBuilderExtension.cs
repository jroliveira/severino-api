namespace Severino.Domain.User
{
    using Microsoft.Extensions.DependencyInjection;

    using Severino.Domain.User.Data.Mongo;

    public static class IdentityServerBuilderExtension
    {
        public static IIdentityServerBuilder AddUser(this IIdentityServerBuilder @this) => @this
            .AddUserWithMongo();
    }
}
