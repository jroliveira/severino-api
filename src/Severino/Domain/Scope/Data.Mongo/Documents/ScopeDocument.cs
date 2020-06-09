namespace Severino.Domain.Scope.Data.Mongo.Documents
{
    using MongoDB.Bson.Serialization.Attributes;

    using Severino.Infrastructure.Data.Mongo;
    using Severino.Infrastructure.Monad;

    using static Severino.Domain.Scope.Scope;
    using static Severino.Infrastructure.Monad.Utils.Util;

    [BsonIgnoreExtraElements]
    internal sealed class ScopeDocument : IDocument
    {
        public string? Name { get; set; }

        public static ScopeDocument ParseDocument(Scope entity) => new ScopeDocument
        {
            Name = entity.Value,
        };

        public override string? ToString() => this.Name;

        public IdentityServer4.Models.Scope ToIdentityModel() => new IdentityServer4.Models.Scope(this.Name);

        public Try<Scope> ToEntity() => NewScope(this.Name == null
            ? None()
            : Some(this.Name));
    }
}
