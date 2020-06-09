namespace Severino.WebApi.Features.Home.Hal
{
    using System.Collections.Generic;

    using Severino.WebApi.Features.Shared.Hal;
    using Severino.WebApi.Infrastructure.Hal.Link;

    using static System.Net.Http.HttpMethod;

    internal sealed class HomeResourceBuilder : ResourceBuilder
    {
        internal override IReadOnlyCollection<IResourceConfiguration> Configure() => new List<IResourceConfiguration>
        {
            new ResourceConfiguration<HomeModel>((_, model) => new List<Link>
            {
                new Link("/v1", "self", Get),
            }),
        };
    }
}
