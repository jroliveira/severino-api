namespace Severino.Domain.Resource
{
    using System.Collections.Generic;

    using Severino.Domain.Claim;
    using Severino.Domain.Shared;

    public abstract class Resource : Entity<string>
    {
        protected Resource(
            string name,
            string displayName,
            IReadOnlyCollection<Claim> claims)
            : base(name)
        {
            this.DisplayName = displayName;
            this.Claims = claims;
        }

        public string DisplayName { get; }

        public IReadOnlyCollection<Claim> Claims { get; }
    }
}
