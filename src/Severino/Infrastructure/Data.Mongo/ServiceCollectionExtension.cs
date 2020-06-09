namespace Severino.Infrastructure.Data.Mongo
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureMongo(this IServiceCollection @this, IConfiguration configuration) => @this
            .AddScoped<MongoConnection>()
            .Configure<MongoConfiguration>(configuration.GetSection("mongo"));
    }
}
