namespace Severino.WebApi.Features.ApiResource.Hal
{
    using System.Collections.Generic;

    using Severino.WebApi.Features.Shared.Hal;
    using Severino.WebApi.Infrastructure.Hal.Link;

    using static System.Net.Http.HttpMethod;

    internal sealed class ApiResourceResourceBuilder : ResourceBuilder
    {
        internal override IReadOnlyCollection<IResourceConfiguration> Configure() => new List<IResourceConfiguration>
        {
            new ResourceConfiguration<ApiResourceModel>((_, model) => new List<Link>
            {
                new Link($"/v1/api-resources/{model.Name}", "self", Get),
                new Link("/v1/api-resources", "all", Get),
                new Link("/v1/api-resources", "create-api-resource", Post),
                new Link($"/v1/api-resources/{model.Name}", "update-api-resource", Put),
                new Link($"/v1/api-resources/{model.Name}", "delete-api-resource", Delete),
            }),

            new PageResourceConfiguration<ApiResourceModel>(
                getItemsLinks: (_, model) => new List<Link>
                {
                    new Link($"/v1/api-resources/{model.Name}", "self", Get),
                    new Link("/v1/api-resources", "all", Get),
                    new Link("/v1/api-resources", "create-payee", Post),
                    new Link($"/v1/api-resources/{model.Name}", "update-api-resource", Put),
                    new Link($"/v1/api-resources/{model.Name}", "delete-api-resource", Delete),
                },
                getLinks: (_, model) => new List<Link>
                {
                    new Link("/v1/api-resources", "self", Get),
                }),
        };
    }
}
