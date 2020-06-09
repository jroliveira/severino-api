namespace Severino.Domain.Client.Data.Mongo.Documents
{
    using System.Collections.Generic;
    using System.Linq;

    using MongoDB.Bson.Serialization.Attributes;

    using Severino.Domain.Scope.Data.Mongo.Documents;
    using Severino.Infrastructure.Data.Mongo;

    [BsonIgnoreExtraElements]
    internal sealed class ClientDocument : IDocument
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public IEnumerable<SecretDocument> Secrets { get; set; } = new List<SecretDocument>();

        public IReadOnlyCollection<ScopeDocument> Scopes { get; set; } = new List<ScopeDocument>();

        public IReadOnlyCollection<GrantTypeDocument> GrantTypes { get; set; } = new List<GrantTypeDocument>();

        public static implicit operator IdentityServer4.Models.Client?(ClientDocument document) => document?.ToIdentityModel();

        public IdentityServer4.Models.Client ToIdentityModel() => new IdentityServer4.Models.Client
        {
            ClientId = this.Id,
            ClientName = this.Name,
            ClientSecrets = this.Secrets.Select(item => item.ToIdentityModel()).ToList(),
            AllowedScopes = this.Scopes.Select(item => item.ToString()).ToList(),
            AllowedGrantTypes = this.GrantTypes.Select(item => item.ToString()).ToList(),
        };
    }
}
