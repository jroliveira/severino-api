﻿namespace Severino.WebApi.Features.Home.Hal
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
                new Link("/v1/clients", "clients", Get),
                new Link("/v1/api-resources", "api-resources", Get),
                new Link("/v1/identity-resources", "identity-resources", Get),
                new Link("/v1/users", "users", Get),
            }),
        };
    }
}
