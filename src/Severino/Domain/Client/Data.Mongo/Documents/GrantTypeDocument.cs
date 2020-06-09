namespace Severino.Domain.Client.Data.Mongo.Documents
{
    using MongoDB.Bson.Serialization.Attributes;

    using Severino.Infrastructure.Data.Mongo;
    using Severino.Infrastructure.Monad;

    using static Severino.Domain.Client.GrantType;
    using static Severino.Infrastructure.Monad.Utils.Util;

    [BsonIgnoreExtraElements]
    internal sealed class GrantTypeDocument : IDocument
    {
        public string? Name { get; set; }

        public static GrantTypeDocument ParseDocument(GrantType entity) => new GrantTypeDocument
        {
            Name = entity.Value,
        };

        public override string? ToString() => this.Name;

        public Try<GrantType> ToEntity() => NewGrantType(this.Name == null
            ? None()
            : Some(this.Name));
    }
}
