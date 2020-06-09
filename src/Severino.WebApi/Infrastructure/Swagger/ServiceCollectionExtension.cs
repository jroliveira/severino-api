namespace Severino.WebApi.Infrastructure.Swagger
{
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using NSwag;

    using static Severino.Infrastructure.Serialization.JsonSettings;

    internal static class ServiceCollectionExtension
    {
        internal static IServiceCollection ConfigureSwagger(this IServiceCollection @this, IConfiguration configuration)
        {
            var swaggerConfig = configuration
                .GetSection("swagger")
                .Get<SwaggerConfiguration>();

            if (!swaggerConfig.IsEnabled())
            {
                return @this;
            }

            using var provider = @this.BuildServiceProvider();
            var versionDescriptionProvider = provider.GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (var apiVersion in versionDescriptionProvider.ApiVersionDescriptions)
            {
                @this.AddSwaggerDocument(config =>
                {
                    config.SerializerSettings = JsonSerializerSettings;
                    config.DocumentName = $"v{apiVersion.GroupName}";
                    config.ApiGroupNames = new[] { apiVersion.GroupName };
                    config.PostProcess = document => CreateDocument(document, apiVersion);
                });
            }

            return @this;
        }

        private static void CreateDocument(OpenApiDocument document, ApiVersionDescription apiVersion)
        {
            document.Info.Title = "Severino API";
            document.Info.Version = $"v{apiVersion.GroupName}";
            document.Info.Description = "Severino is a Security Token Service.";
            document.Info.Contact = new OpenApiContact
            {
                Name = "Junior Oliveira",
                Email = "junolive@gmail.com",
            };

            document.Info.License = new OpenApiLicense
            {
                Name = "AGPL-3.0",
                Url = "https://opensource.org/licenses/AGPL-3.0",
            };

            if (apiVersion.IsDeprecated)
            {
                document.Info.Description += "Severino is a Security Token Service. THIS API VERSION HAS BEEN DEPRECATED.";
            }
        }
    }
}
