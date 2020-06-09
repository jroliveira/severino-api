namespace Severino.Domain.Resource.Data.Mongo.Stores
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using IdentityServer4.Models;
    using IdentityServer4.Stores;

    using Severino.Domain.Resource.Data.Mongo.Documents;
    using Severino.Infrastructure.Data.Mongo;

    public sealed class ResourceStore : IResourceStore
    {
        private readonly MongoConnection connection;

        public ResourceStore(MongoConnection connection) => this.connection = connection;

        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var resources = await this.connection
                .Where<ResourceDocument>(resource => resource.Type == "IdentityResource" && scopeNames
                    .Contains(resource.Name));

            return resources
                .Select(resource => resource.ToIdentityResourceIdentityModel());
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var resources = await this.connection
                .Where<ResourceDocument>(resource => resource.Type == "ApiResource" && resource
                    .Scopes
                    .Any(scope => scopeNames
                        .Contains(scope.Name)));

            return resources
                .Select(resource => resource.ToApiResourceIdentityModel());
        }

        public async Task<ApiResource?> FindApiResourceAsync(string name)
        {
            var document = await this.connection
                .SingleOrDefault<ResourceDocument>(
                    resource => resource.Type == "IdentityResource" && resource.Name == name);

            return document.Get();
        }

        public async Task<Resources> GetAllResourcesAsync()
        {
            var identityResources = await this.connection
                .Where<ResourceDocument>(resource => resource.Type == "IdentityResource");

            var apiResources = await this.connection
                .Where<ResourceDocument>(resource => resource.Type == "ApiResource");

            return new Resources(
                identityResources.Select(resource => resource.ToIdentityResourceIdentityModel()),
                apiResources.Select(resource => resource.ToApiResourceIdentityModel()));
        }
    }
}
