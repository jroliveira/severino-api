namespace Severino.WebApi.Features.Client.Hal
{
    using System.Collections.Generic;

    using Severino.WebApi.Features.Shared.Hal;
    using Severino.WebApi.Infrastructure.Hal.Link;

    using static System.Net.Http.HttpMethod;

    internal sealed class ClientResourceBuilder : ResourceBuilder
    {
        internal override IReadOnlyCollection<IResourceConfiguration> Configure() => new List<IResourceConfiguration>
        {
            new ResourceConfiguration<ClientModel>((_, model) => new List<Link>
            {
                new Link($"/v1/clients/{model.Id}", "self", Get),
                new Link("/v1/clients", "all", Get),
                new Link("/v1/clients", "create-client", Post),
                new Link($"/v1/clients/{model.Id}", "update-client", Put),
                new Link($"/v1/clients/{model.Id}", "delete-client", Delete),
            }),

            new PageResourceConfiguration<ClientModel>(
                getItemsLinks: (_, model) => new List<Link>
                {
                    new Link($"/v1/clients/{model.Id}", "self", Get),
                    new Link("/v1/clients", "all", Get),
                    new Link("/v1/clients", "create-payee", Post),
                    new Link($"/v1/clients/{model.Id}", "update-client", Put),
                    new Link($"/v1/clients/{model.Id}", "delete-client", Delete),
                },
                getLinks: (_, model) => new List<Link>
                {
                    new Link("/v1/clients", "self", Get),
                }),
        };
    }
}
