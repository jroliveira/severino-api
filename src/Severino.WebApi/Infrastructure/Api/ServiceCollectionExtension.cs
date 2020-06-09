namespace Severino.WebApi.Infrastructure.Api
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    using Severino.WebApi.Infrastructure.Hal;
    using Severino.WebApi.Infrastructure.Versioning;

    using static Severino.Infrastructure.Serialization.JsonSettings;

    internal static class ServiceCollectionExtension
    {
        internal static IServiceCollection ConfigureApi(this IServiceCollection @this)
        {
            @this.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            @this
                .AddResponseCompression()
                .AddResponseCaching()
                .AddCors(options => options.AddPolicy("CorsPolicy", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()))
                .AddMvcCore(options =>
                {
                    options.EnableEndpointRouting = false;
                    options.AddApiVersionRoutePrefixConvention();
                })
                .AddApiExplorer()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = JsonSerializerSettings.ContractResolver;
                    options.SerializerSettings.Formatting = JsonSerializerSettings.Formatting;
                    options.SerializerSettings.Culture = JsonSerializerSettings.Culture;
                    options.SerializerSettings.NullValueHandling = JsonSerializerSettings.NullValueHandling;
                    options.SerializerSettings.ReferenceLoopHandling = JsonSerializerSettings.ReferenceLoopHandling;
                    options.SerializerSettings.DateTimeZoneHandling = JsonSerializerSettings.DateTimeZoneHandling;
                    options.SerializerSettings.DateFormatHandling = JsonSerializerSettings.DateFormatHandling;

                    foreach (var converter in JsonSerializerSettings.Converters)
                    {
                        options.SerializerSettings.Converters.Add(converter);
                    }
                })
                .AddHal(JsonSerializerSettings);

            return @this;
        }
    }
}
