namespace Severino.Domain.Client.Data.Mongo
{
    using Microsoft.Extensions.DependencyInjection;

    using Severino.Domain.Client.Commands;
    using Severino.Domain.Client.Data.Mongo.Commands;
    using Severino.Domain.Client.Data.Mongo.Queries;
    using Severino.Domain.Client.Queries;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureClientWithMongo(this IServiceCollection @this) => @this
            .AddScoped<IUpsertClient, UpsertClient>()
            .AddScoped<IDeleteClient, DeleteClient>()
            .AddScoped<IGetClients, GetClients>()
            .AddScoped<IGetClientById, GetClientById>();
    }
}
