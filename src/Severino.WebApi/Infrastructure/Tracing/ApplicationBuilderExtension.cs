namespace Severino.WebApi.Infrastructure.Tracing
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;

    using Severino.Infrastructure.Tracing.Configurations;

    internal static class ApplicationBuilderExtension
    {
        internal static IApplicationBuilder UseTracing(this IApplicationBuilder @this, IConfiguration configuration)
        {
            var tracingConfig = configuration
                .GetSection("tracing")
                .Get<TracingConfiguration>();

            if (!tracingConfig.IsEnabled())
            {
                return @this;
            }

            return @this
                .UseMiddleware<RequestMonitoringMiddleware>();
        }
    }
}
