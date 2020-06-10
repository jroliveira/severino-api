namespace Severino.Domain.Resource.Data.Mongo.Documents
{
    using System.Collections.Generic;
    using System.Linq;

    using MongoDB.Bson.Serialization.Attributes;

    using Severino.Domain.Claim.Data.Mongo.Documents;
    using Severino.Domain.Scope.Data.Mongo.Documents;
    using Severino.Infrastructure.Data.Mongo;
    using Severino.Infrastructure.Monad;

    using static Severino.Domain.Resource.ApiResource;
    using static Severino.Infrastructure.Monad.Utils.Util;

    [BsonIgnoreExtraElements]
    internal sealed class ResourceDocument : IDocument
    {
        public string? Name { get; set; }

        public string? DisplayName { get; set; }

        public bool Emphasize { get; set; }

        public string? Type { get; set; }

        public IReadOnlyCollection<ClaimDocument> Claims { get; set; } = new List<ClaimDocument>();

        public IEnumerable<ScopeDocument> Scopes { get; set; } = new List<ScopeDocument>();

        public static implicit operator IdentityServer4.Models.ApiResource?(ResourceDocument document) => document?.ToApiResourceIdentityModel();

        public static implicit operator ResourceDocument(ApiResource entity) => ParseDocument(entity);

        public static ResourceDocument ParseDocument(ApiResource entity) => new ResourceDocument
        {
            Name = entity.Id,
            DisplayName = entity.DisplayName,
            Type = "ApiResource",
            Claims = entity.Claims.Select(ClaimDocument.ParseDocument).ToList(),
            Scopes = entity.Scopes.Select(ScopeDocument.ParseDocument).ToList(),
        };

        public IdentityServer4.Models.ApiResource ToApiResourceIdentityModel() => new IdentityServer4.Models.ApiResource
        {
            Name = this.Name,
            DisplayName = this.DisplayName,
            UserClaims = this.Claims.Select(item => item.ToString()).ToList(),
            Scopes = this.Scopes.Select(item => item.ToIdentityModel()).ToList(),
        };

        public IdentityServer4.Models.IdentityResource ToIdentityResourceIdentityModel() => new IdentityServer4.Models.IdentityResource
        {
            Name = this.Name,
            DisplayName = this.DisplayName,
            Emphasize = this.Emphasize,
            UserClaims = this.Claims.Select(item => item.ToString()).ToList(),
        };

        public Try<ApiResource> ToApiResourceEntity() => NewApiResource(
            this.Name == null ? None() : Some(this.Name),
            this.DisplayName == null ? None() : Some(this.DisplayName),
            this.Scopes.Select(item => item.ToEntity().Get()).ToList(),
            this.Claims.Select(item => item.ToEntity().Get()).ToList());
    }
}
