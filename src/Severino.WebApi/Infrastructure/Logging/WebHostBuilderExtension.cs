namespace Severino.WebApi.Infrastructure.Logging
{
    using System;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    using Serilog;

    using Severino.Infrastructure.Logging.Configurations;
    using Severino.Infrastructure.Logging.Methods;

    using static System.Enum;

    using static Severino.Infrastructure.Logging.Logger;

    using LogLevel = Severino.Infrastructure.Logging.LogLevel;

    internal static class WebHostBuilderExtension
    {
        internal static IWebHostBuilder ConfigureLogging(this IWebHostBuilder @this) => @this
            .UseSerilog((hostingContext, _) =>
            {
                var logConfig = hostingContext.Configuration
                    .GetSection("log")
                    .Get<LogConfiguration>();

                if (!TryParse(logConfig.Level, out LogLevel level))
                {
                    throw new InvalidCastException($"Log level '{logConfig.Level}' is not valid.");
                }

                NewLogger(
                    level,
                    new SerilogLogMethod(logConfig).Write);
            });
    }
}
