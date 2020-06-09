namespace Severino.WebApi.Features.Shared.Hal
{
    using System;

    using Microsoft.AspNetCore.Http;

    using Severino.WebApi.Infrastructure.Hal.Resource;

    internal interface IResourceConfiguration
    {
        Type Type { get; }

        Func<HttpContext, object, IResource> GetBuilder { get; }
    }
}
