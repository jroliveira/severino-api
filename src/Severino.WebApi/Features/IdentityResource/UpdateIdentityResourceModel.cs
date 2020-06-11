namespace Severino.WebApi.Features.IdentityResource
{
    using System.ComponentModel.DataAnnotations;

    using Severino.Domain.Resource;
    using Severino.Infrastructure.Monad;

    using static Severino.Domain.Resource.IdentityResource;

    public class UpdateIdentityResourceModel
    {
        public UpdateIdentityResourceModel(string displayName, bool emphasize)
        {
            this.DisplayName = displayName;
            this.Emphasize = emphasize;
        }

        [Required]
        public string DisplayName { get; }

        [Required]
        public bool Emphasize { get; }

        public Option<IdentityResource> ToEntity(string name) => NewIdentityResource(name, this.DisplayName, this.Emphasize);
    }
}
