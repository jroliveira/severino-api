namespace Severino.WebApi.Features.ApiResource
{
    using System.ComponentModel.DataAnnotations;

    using Severino.Domain.Resource;
    using Severino.Infrastructure.Monad;

    using static Severino.Domain.Resource.ApiResource;

    public class UpdateApiResourceModel
    {
        public UpdateApiResourceModel(string displayName) => this.DisplayName = displayName;

        [Required]
        public string DisplayName { get; }

        public Option<ApiResource> ToEntity(string name) => NewApiResource(name, this.DisplayName);
    }
}
