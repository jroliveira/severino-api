namespace Severino.WebApi.Features.Shared.Hal
{
    using System;

    using Microsoft.AspNetCore.Http;

    using Severino.Infrastructure.Monad;
    using Severino.WebApi.Infrastructure.Hal.Link;
    using Severino.WebApi.Infrastructure.Hal.Resource;

    using static Severino.WebApi.Features.Shared.Hal.DefaultLinks;

    internal sealed class ResourceConfiguration<TModel> : IResourceConfiguration
    {
        internal ResourceConfiguration(Func<HttpContext, TModel, Links> getLinks) => this.GetBuilder = (context, @object) =>
        {
            var model = (Try<TModel>)@object;

            return new Resource<Try<TModel>>(
                model,
                model.Match(
                    _ => DocumentationLinks,
                    item => getLinks(context, item)));
        };

        public Type Type => typeof(Try<TModel>);

        public Func<HttpContext, object, IResource> GetBuilder { get; }
    }
}
