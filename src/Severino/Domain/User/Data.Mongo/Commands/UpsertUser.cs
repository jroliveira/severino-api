namespace Severino.Domain.User.Data.Mongo.Commands
{
    using System.Threading.Tasks;

    using Severino.Domain.Shared;
    using Severino.Domain.Shared.Commands;
    using Severino.Domain.User.Commands;
    using Severino.Domain.User.Data.Mongo.Documents;
    using Severino.Infrastructure.Data.Mongo;
    using Severino.Infrastructure.Monad;

    using static Severino.Infrastructure.Monad.Utils.Util;

    internal sealed class UpsertUser : Command<UpsertParam<Email, User>>, IUpsertUser
    {
        private readonly MongoConnection connection;

        public UpsertUser(MongoConnection connection) => this.connection = connection;

        protected override async Task<Try<Unit>> Execute(UpsertParam<Email, User> param)
        {
            await this.connection
                .Upsert<UserDocument>(
                    item => item.Email == param.Id,
                    param.Entity);

            return Unit();
        }
    }
}
