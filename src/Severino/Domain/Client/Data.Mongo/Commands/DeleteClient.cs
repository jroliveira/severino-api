namespace Severino.Domain.Client.Data.Mongo.Commands
{
    using System.Threading.Tasks;

    using Severino.Domain.Client.Commands;
    using Severino.Domain.Client.Data.Mongo.Documents;
    using Severino.Domain.Shared.Commands;
    using Severino.Infrastructure.Data.Mongo;
    using Severino.Infrastructure.Monad;

    using static Severino.Infrastructure.Monad.Utils.Util;

    internal sealed class DeleteClient : Command<DeleteParam<string>>, IDeleteClient
    {
        private readonly MongoConnection connection;

        public DeleteClient(MongoConnection connection) => this.connection = connection;

        protected override async Task<Try<Unit>> Execute(DeleteParam<string> param)
        {
            await this.connection
                .RemoveAll<ClientDocument>(client => client.Id == param.Id);

            return Unit();
        }
    }
}
