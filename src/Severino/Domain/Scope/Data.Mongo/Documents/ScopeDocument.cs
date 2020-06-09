namespace Severino.Domain.Scope.Data.Mongo.Documents
{
    using MongoDB.Bson.Serialization.Attributes;

    using Severino.Infrastructure.Data.Mongo;

    [BsonIgnoreExtraElements]
    internal sealed class ScopeDocument : IDocument
    {
        public string? Name { get; set; }

        public override string? ToString() => this.Name;

        public IdentityServer4.Models.Scope ToIdentityModel() => new IdentityServer4.Models.Scope(this.Name);
    }
}
