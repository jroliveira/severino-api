namespace Severino.Domain.Claim.Data.Mongo.Documents
{
    using MongoDB.Bson.Serialization.Attributes;

    using Severino.Infrastructure.Data.Mongo;
    using Severino.Infrastructure.Monad;

    using static Severino.Domain.Claim.Claim;
    using static Severino.Infrastructure.Monad.Utils.Util;

    [BsonIgnoreExtraElements]
    internal sealed class ClaimDocument : IDocument
    {
        public string? Name { get; set; }

        public static ClaimDocument ParseDocument(Claim entity) => new ClaimDocument
        {
            Name = entity.Value,
        };

        public override string? ToString() => this.Name;

        public Try<Claim> ToEntity() => NewClaim(this.Name == null
            ? None()
            : Some(this.Name));
    }
}
