namespace Severino.Domain.Resource
{
    using System.Collections.Generic;

    using Severino.Domain.Claim;
    using Severino.Infrastructure.ErrorHandling.Exceptions;
    using Severino.Infrastructure.Monad;

    using static Severino.Infrastructure.Monad.Utils.Util;

    public sealed class IdentityResource : Resource
    {
        private IdentityResource(
            string name,
            string displayName,
            bool emphasize,
            IReadOnlyCollection<Claim> claims)
            : base(name, displayName, claims) => this.Emphasize = emphasize;

        public bool Emphasize { get; }

        public static Try<IdentityResource> NewIdentityResource(
            in Option<string> name,
            in Option<string> displayName,
            in Option<bool> emphasize) => NewIdentityResource(
                name,
                displayName,
                emphasize,
                new List<Claim>());

        public static Try<IdentityResource> NewIdentityResource(
            in Option<string> name,
            in Option<string> displayName,
            in Option<bool> emphasize,
            in Option<IReadOnlyCollection<Claim>> claims) =>
                name
                && displayName
                && emphasize
                && claims
                    ? new IdentityResource(
                        name.Get(),
                        displayName.Get(),
                        emphasize.Get(),
                        claims.Get())
                    : Failure<IdentityResource>(new InvalidObjectException("Invalid identity resource."));
    }
}
