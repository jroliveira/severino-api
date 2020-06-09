namespace Severino.WebApi.Infrastructure.ErrorHandling
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    using Severino.Infrastructure.ErrorHandling.Exceptions;
    using Severino.WebApi.Infrastructure.Hal;

    using static Newtonsoft.Json.JsonConvert;

    using static Severino.Infrastructure.ErrorHandling.ExceptionHandler;
    using static Severino.Infrastructure.Logging.Logger;
    using static Severino.Infrastructure.Serialization.JsonSettings;
    using static Severino.WebApi.Infrastructure.Hal.ResourceBuilders;

    internal sealed class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next) => this.next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);

                switch (context.Response.StatusCode)
                {
                    case 404:
                        await WriteResponse(context, new NotFoundException($"The HTTP resource that matches the request URI '{context.Request.Scheme}:/{context.Request.Path.Value}' not found."), 404);
                        break;
                }
            }
            catch (Exception exception)
            {
                await WriteResponse(context, exception, 500);
            }
        }

        private static async Task WriteResponse(HttpContext context, Exception exception, int statusCode)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var serializerSettings = JsonSerializerSettings;
            serializerSettings.AddHal();

            LogError("An unhandled error has occurred.", exception);

            var errorModel = HandleException(exception);

            await context.Response.WriteAsync(SerializeObject(
                GetResource(context, errorModel, errorModel.GetType()),
                serializerSettings));
        }
    }
}
