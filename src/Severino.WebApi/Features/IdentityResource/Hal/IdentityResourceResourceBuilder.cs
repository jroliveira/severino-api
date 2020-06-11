namespace Severino.WebApi.Features.IdentityResource.Hal
{
    using System.Collections.Generic;

    using Severino.WebApi.Features.Shared.Hal;
    using Severino.WebApi.Infrastructure.Hal.Link;

    using static System.Net.Http.HttpMethod;

    internal sealed class IdentityResourceResourceBuilder : ResourceBuilder
    {
        internal override IReadOnlyCollection<IResourceConfiguration> Configure() => new List<IResourceConfiguration>
        {
            new ResourceConfiguration<IdentityResourceModel>((_, model) => new List<Link>
            {
                new Link($"/v1/identity-resources/{model.Name}", "self", Get),
                new Link("/v1/identity-resources", "all", Get),
                new Link("/v1/identity-resources", "create-identity-resource", Post),
                new Link($"/v1/identity-resources/{model.Name}", "update-identity-resource", Put),
                new Link($"/v1/identity-resources/{model.Name}", "delete-identity-resource", Delete),
            }),

            new PageResourceConfiguration<IdentityResourceModel>(
                getItemsLinks: (_, model) => new List<Link>
                {
                    new Link($"/v1/identity-resources/{model.Name}", "self", Get),
                    new Link("/v1/identity-resources", "all", Get),
                    new Link("/v1/identity-resources", "create-payee", Post),
                    new Link($"/v1/identity-resources/{model.Name}", "update-identity-resource", Put),
                    new Link($"/v1/identity-resources/{model.Name}", "delete-identity-resource", Delete),
                },
                getLinks: (_, model) => new List<Link>
                {
                    new Link("/v1/identity-resources", "self", Get),
                }),
        };
    }
}
