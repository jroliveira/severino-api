namespace Severino.Domain.Client.Data.Mongo.Documents
{
    using System.Collections.Generic;
    using System.Linq;

    using MongoDB.Bson.Serialization.Attributes;

    using Severino.Domain.Scope.Data.Mongo.Documents;
    using Severino.Infrastructure.Data.Mongo;
    using Severino.Infrastructure.Monad;

    using static Severino.Domain.Client.Client;
    using static Severino.Infrastructure.Monad.Utils.Util;

    [BsonIgnoreExtraElements]
    internal sealed class ClientDocument : IDocument
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public IEnumerable<SecretDocument> Secrets { get; set; } = new List<SecretDocument>();

        public IReadOnlyCollection<ScopeDocument> Scopes { get; set; } = new List<ScopeDocument>();

        public IReadOnlyCollection<GrantTypeDocument> GrantTypes { get; set; } = new List<GrantTypeDocument>();

        public static implicit operator IdentityServer4.Models.Client?(ClientDocument document) => document?.ToIdentityModel();

        public static implicit operator ClientDocument(Client entity) => ParseDocument(entity);

        public static ClientDocument ParseDocument(Client entity) => new ClientDocument
        {
            Id = entity.Id,
            Name = entity.Name,
            Secrets = entity.Secrets.Select(SecretDocument.ParseDocument).ToList(),
            Scopes = entity.Scopes.Select(ScopeDocument.ParseDocument).ToList(),
            GrantTypes = entity.GrantTypes.Select(GrantTypeDocument.ParseDocument).ToList(),
        };

        public IdentityServer4.Models.Client ToIdentityModel() => new IdentityServer4.Models.Client
        {
            ClientId = this.Id,
            ClientName = this.Name,
            ClientSecrets = this.Secrets.Select(item => item.ToIdentityModel()).ToList(),
            AllowedScopes = this.Scopes.Select(item => item.ToString()).ToList(),
            AllowedGrantTypes = this.GrantTypes.Select(item => item.ToString()).ToList(),
        };

        public Try<Client> ToEntity() => NewClient(
            this.Id == null ? None() : Some(this.Id),
            this.Name == null ? None() : Some(this.Name),
            this.Secrets.Select(item => item.ToEntity().Get()).ToList(),
            this.Scopes.Select(item => item.ToEntity().Get()).ToList(),
            this.GrantTypes.Select(item => item.ToEntity().Get()).ToList());
    }
}
