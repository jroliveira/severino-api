namespace Severino.Domain.Client.Data.Mongo.Documents
{
    using MongoDB.Bson.Serialization.Attributes;

    using Severino.Infrastructure.Data.Mongo;
    using Severino.Infrastructure.Monad;

    using static Severino.Domain.Client.Secret;
    using static Severino.Infrastructure.Monad.Utils.Util;

    [BsonIgnoreExtraElements]
    internal sealed class SecretDocument : IDocument
    {
        public string? Value { get; set; }

        public static SecretDocument ParseDocument(Secret entity) => new SecretDocument
        {
            Value = entity.Value,
        };

        public IdentityServer4.Models.Secret? ToIdentityModel() => new IdentityServer4.Models.Secret(this.Value);

        public Try<Secret> ToEntity() => NewSecret(this.Value == null
            ? None()
            : Some(this.Value));
    }
}
