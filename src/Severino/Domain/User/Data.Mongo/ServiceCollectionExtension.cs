namespace Severino.Domain.User.Data.Mongo
{
    using Microsoft.Extensions.DependencyInjection;

    using Severino.Domain.User.Commands;
    using Severino.Domain.User.Data.Mongo.Commands;
    using Severino.Domain.User.Data.Mongo.Queries;
    using Severino.Domain.User.Queries;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureUserWithMongo(this IServiceCollection @this) => @this
            .AddScoped<IUpsertUser, UpsertUser>()
            .AddScoped<IDeleteUser, DeleteUser>()
            .AddScoped<IGetUsers, GetUsers>()
            .AddScoped<IGetUserByEmail, GetUserByEmail>();
    }
}
