namespace Severino.Domain.Client.Data.Mongo.Documents
{
    using MongoDB.Bson.Serialization.Attributes;

    using Severino.Infrastructure.Data.Mongo;

    [BsonIgnoreExtraElements]
    internal sealed class SecretDocument : IDocument
    {
        public string? Value { get; set; }

        public IdentityServer4.Models.Secret? ToIdentityModel() => new IdentityServer4.Models.Secret(this.Value);
    }
}
