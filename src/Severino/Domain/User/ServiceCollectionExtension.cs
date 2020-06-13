namespace Severino.Domain.User
{
    using Microsoft.Extensions.DependencyInjection;

    using Severino.Domain.User.Data.Mongo;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureUser(this IServiceCollection @this) => @this
            .ConfigureUserWithMongo();
    }
}
