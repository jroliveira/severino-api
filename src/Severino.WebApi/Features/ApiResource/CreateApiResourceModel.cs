namespace Severino.WebApi.Features.ApiResource
{
    using System.ComponentModel.DataAnnotations;

    using Severino.Domain.Resource;
    using Severino.Infrastructure.Monad;

    using static Severino.Domain.Resource.ApiResource;

    public class CreateApiResourceModel
    {
        public CreateApiResourceModel(
            string name,
            string displayName)
        {
            this.Name = name;
            this.DisplayName = displayName;
        }

        [Required]
        public string Name { get; }

        [Required]
        public string DisplayName { get; }

        public static implicit operator Option<ApiResource>(CreateApiResourceModel model) => NewApiResource(model.Name, model.DisplayName);
    }
}
