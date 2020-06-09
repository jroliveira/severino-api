namespace Severino.WebApi.Features.Client
{
    using System.Collections.Generic;
    using System.Linq;

    using Severino.Domain.Client;

    public sealed class ClientModel
    {
        private ClientModel(Client entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Secrets = entity.Secrets.Select(secret => secret.Value);
            this.Scopes = entity.Scopes.Select(scope => scope.Value);
            this.GrantTypes = entity.GrantTypes.Select(grantType => grantType.Value);
        }

        public string Id { get; }

        public string Name { get; }

        public IEnumerable<string> Secrets { get; }

        public IEnumerable<string> Scopes { get; }

        public IEnumerable<string> GrantTypes { get; }

        internal static ClientModel NewClientModel(Client entity) => new ClientModel(entity);
    }
}
