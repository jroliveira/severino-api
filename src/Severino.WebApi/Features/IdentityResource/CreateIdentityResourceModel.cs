namespace Severino.WebApi.Features.IdentityResource
{
    using System.ComponentModel.DataAnnotations;

    using Severino.Domain.Resource;
    using Severino.Infrastructure.Monad;

    using static Severino.Domain.Resource.IdentityResource;

    public class CreateIdentityResourceModel
    {
        public CreateIdentityResourceModel(
            string name,
            string displayName,
            bool emphasize)
        {
            this.Name = name;
            this.DisplayName = displayName;
            this.Emphasize = emphasize;
        }

        [Required]
        public string Name { get; }

        [Required]
        public string DisplayName { get; }

        [Required]
        public bool Emphasize { get; }

        public static implicit operator Option<IdentityResource>(CreateIdentityResourceModel model) => NewIdentityResource(
            model.Name,
            model.DisplayName,
            model.Emphasize);
    }
}
