namespace Severino.WebApi.Features.IdentityResource
{
    using System.Collections.Generic;
    using System.Linq;

    using Severino.Domain.Resource;

    public sealed class IdentityResourceModel
    {
        private IdentityResourceModel(IdentityResource entity)
        {
            this.Name = entity.Id;
            this.DisplayName = entity.DisplayName;
            this.Emphasize = entity.Emphasize;
            this.Claims = entity.Claims.Select(claim => claim.Value);
        }

        public string Name { get; }

        public string DisplayName { get; }

        public bool Emphasize { get; }

        public IEnumerable<string> Claims { get; }

        internal static IdentityResourceModel NewIdentityResourceModel(IdentityResource entity) => new IdentityResourceModel(entity);
    }
}
