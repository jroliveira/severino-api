﻿namespace Severino.WebApi.Infrastructure.ErrorHandling
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;

    using static Severino.WebApi.Infrastructure.ErrorHandling.ErrorHandler;

    internal static class ApplicationBuilderExtension
    {
        internal static IApplicationBuilder UseErrorHandling(this IApplicationBuilder @this, IWebHostEnvironment environment)
        {
            NewErrorHandler(environment);

            return @this
                .UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
