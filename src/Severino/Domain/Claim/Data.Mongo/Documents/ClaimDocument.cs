namespace Severino.Domain.Claim.Data.Mongo.Documents
{
    using MongoDB.Bson.Serialization.Attributes;

    using Severino.Infrastructure.Data.Mongo;

    [BsonIgnoreExtraElements]
    internal sealed class ClaimDocument : IDocument
    {
        public string? Name { get; set; }

        public override string? ToString() => this.Name;
    }
}
