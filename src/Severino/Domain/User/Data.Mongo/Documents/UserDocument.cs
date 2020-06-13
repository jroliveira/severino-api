namespace Severino.Domain.User.Data.Mongo.Documents
{
    using MongoDB.Bson.Serialization.Attributes;

    using Severino.Infrastructure.Data.Mongo;
    using Severino.Infrastructure.Monad;

    using static Severino.Domain.Shared.Email;
    using static Severino.Domain.User.User;
    using static Severino.Infrastructure.Monad.Utils.Util;

    [BsonIgnoreExtraElements]
    internal sealed class UserDocument : IDocument
    {
        public string? Email { get; set; }

        public string? Password { get; set; }

        public static implicit operator UserDocument(User entity) => ParseDocument(entity);

        public static UserDocument ParseDocument(User entity) => new UserDocument
        {
            Email = entity.Id,
            Password = entity.Password,
        };

        public Try<User> ToEntity() => NewUser(
            NewEmail(this.Email == null ? None() : Some(this.Email)),
            this.Password == null ? None() : Some(this.Password));
    }
}
