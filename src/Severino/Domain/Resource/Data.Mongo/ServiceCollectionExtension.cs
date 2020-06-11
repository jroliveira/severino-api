namespace Severino.Domain.Resource.Data.Mongo
{
    using Microsoft.Extensions.DependencyInjection;

    using Severino.Domain.Resource.Commands;
    using Severino.Domain.Resource.Data.Mongo.Commands;
    using Severino.Domain.Resource.Data.Mongo.Queries;
    using Severino.Domain.Resource.Queries;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureResourceWithMongo(this IServiceCollection @this) => @this
            .AddScoped<IDeleteResource, DeleteResource>()
            .AddScoped<IUpsertApiResource, UpsertApiResource>()
            .AddScoped<IGetApiResources, GetApiResources>()
            .AddScoped<IGetApiResourceByName, GetApiResourceByName>()
            .AddScoped<IUpsertIdentityResource, UpsertIdentityResource>()
            .AddScoped<IGetIdentityResources, GetIdentityResources>()
            .AddScoped<IGetIdentityResourceByName, GetIdentityResourceByName>();
    }
}
