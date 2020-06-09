namespace Severino.WebApi.Features.Shared.Hal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Http;

    using Severino.WebApi.Infrastructure.Hal.Resource;

    internal abstract class ResourceBuilder : IResourceBuilder
    {
        public IReadOnlyDictionary<Type, Func<HttpContext, object, IResource>> Builders => this.Configure().ToDictionary(
            configuration => configuration.Type,
            configuration => configuration.GetBuilder);

        internal abstract IReadOnlyCollection<IResourceConfiguration> Configure();
    }
}
