namespace Severino.Domain.Client
{
    using System.Collections.Generic;

    using Severino.Domain.Scope;
    using Severino.Domain.Shared;
    using Severino.Infrastructure.ErrorHandling.Exceptions;
    using Severino.Infrastructure.Monad;

    using static Severino.Infrastructure.Monad.Utils.Util;

    public sealed class Client : Entity<string>
    {
        private Client(
            string id,
            string name,
            IReadOnlyCollection<Secret> secrets,
            IReadOnlyCollection<Scope> scopes,
            IReadOnlyCollection<GrantType> grantTypes)
            : base(id)
        {
            this.Name = name;
            this.Secrets = secrets;
            this.Scopes = scopes;
            this.GrantTypes = grantTypes;
        }

        public string Name { get; }

        public IReadOnlyCollection<Secret> Secrets { get; }

        public IReadOnlyCollection<Scope> Scopes { get; }

        public IReadOnlyCollection<GrantType> GrantTypes { get; }

        public static Try<Client> NewClient(
            in Option<string> id,
            in Option<string> name) => NewClient(
                id,
                name,
                new List<Secret>(),
                new List<Scope>(),
                new List<GrantType>());

        public static Try<Client> NewClient(
            in Option<string> id,
            in Option<string> name,
            in Option<IReadOnlyCollection<Secret>> secrets,
            in Option<IReadOnlyCollection<Scope>> scopes,
            in Option<IReadOnlyCollection<GrantType>> grantTypes) =>
                id
                && name
                && secrets
                && scopes
                && grantTypes
                    ? new Client(id.Get(), name.Get(), secrets.Get(), scopes.Get(), grantTypes.Get())
                    : Failure<Client>(new InvalidObjectException("Invalid client."));
    }
}
