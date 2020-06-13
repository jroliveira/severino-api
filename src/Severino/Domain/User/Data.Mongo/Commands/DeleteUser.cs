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

    internal sealed class DeleteUser : Command<DeleteParam<Email>>, IDeleteUser
    {
        private readonly MongoConnection connection;

        public DeleteUser(MongoConnection connection) => this.connection = connection;

        protected override async Task<Try<Unit>> Execute(DeleteParam<Email> param)
        {
            await this.connection
                .RemoveAll<UserDocument>(user => user.Email == param.Id);

            return Unit();
        }
    }
}
