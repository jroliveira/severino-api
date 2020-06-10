namespace Severino.WebApi.Features.ApiResource
{
    using System.Collections.Generic;
    using System.Linq;

    using Severino.Domain.Resource;

    public sealed class ApiResourceModel
    {
        private ApiResourceModel(ApiResource entity)
        {
            this.Name = entity.Id;
            this.DisplayName = entity.DisplayName;
            this.Claims = entity.Claims.Select(claim => claim.Value);
            this.Scopes = entity.Scopes.Select(scope => scope.Value);
        }

        public string Name { get; }

        public string DisplayName { get; }

        public IEnumerable<string> Claims { get; }

        public IEnumerable<string> Scopes { get; }

        internal static ApiResourceModel NewApiResourceModel(ApiResource entity) => new ApiResourceModel(entity);
    }
}
