namespace Severino.Domain.Resource
{
    using System.Collections.Generic;

    using Severino.Domain.Claim;
    using Severino.Domain.Scope;
    using Severino.Infrastructure.ErrorHandling.Exceptions;
    using Severino.Infrastructure.Monad;

    using static Severino.Infrastructure.Monad.Utils.Util;

    public sealed class ApiResource : Resource
    {
        private ApiResource(
            string name,
            string displayName,
            IReadOnlyCollection<Scope> scopes,
            IReadOnlyCollection<Claim> claims)
            : base(name, displayName, claims) => this.Scopes = scopes;

        public IReadOnlyCollection<Scope> Scopes { get; }

        public static Try<ApiResource> NewApiResource(
            in Option<string> name,
            in Option<string> displayName) => NewApiResource(
                name,
                displayName,
                new List<Scope>(),
                new List<Claim>());

        public static Try<ApiResource> NewApiResource(
            in Option<string> name,
            in Option<string> displayName,
            in Option<IReadOnlyCollection<Scope>> scopes,
            in Option<IReadOnlyCollection<Claim>> claims) =>
                name
                && displayName
                && scopes
                && claims
                    ? new ApiResource(
                        name.Get(),
                        displayName.Get(),
                        scopes.Get(),
                        claims.Get())
                    : Failure<ApiResource>(new InvalidObjectException("Invalid api resource."));
    }
}
