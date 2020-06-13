namespace Severino.WebApi.Features.User.Hal
{
    using System.Collections.Generic;

    using Severino.WebApi.Features.Shared.Hal;
    using Severino.WebApi.Infrastructure.Hal.Link;

    using static System.Net.Http.HttpMethod;

    internal sealed class UserResourceBuilder : ResourceBuilder
    {
        internal override IReadOnlyCollection<IResourceConfiguration> Configure() => new List<IResourceConfiguration>
        {
            new ResourceConfiguration<UserModel>((_, model) => new List<Link>
            {
                new Link($"/v1/users/{model.Email}", "self", Get),
                new Link("/v1/users", "all", Get),
                new Link("/v1/users", "create-user", Post),
                new Link($"/v1/users/{model.Email}", "update-user", Put),
                new Link($"/v1/users/{model.Email}", "delete-user", Delete),
            }),

            new PageResourceConfiguration<UserModel>(
                getItemsLinks: (_, model) => new List<Link>
                {
                    new Link($"/v1/users/{model.Email}", "self", Get),
                    new Link("/v1/users", "all", Get),
                    new Link("/v1/users", "create-payee", Post),
                    new Link($"/v1/users/{model.Email}", "update-user", Put),
                    new Link($"/v1/users/{model.Email}", "delete-user", Delete),
                },
                getLinks: (_, model) => new List<Link>
                {
                    new Link("/v1/users", "self", Get),
                }),
        };
    }
}
