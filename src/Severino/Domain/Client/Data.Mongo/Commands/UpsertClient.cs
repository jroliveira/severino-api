namespace Severino.Domain.Client.Data.Mongo.Commands
{
    using System.Threading.Tasks;

    using Severino.Domain.Client.Commands;
    using Severino.Domain.Client.Data.Mongo.Documents;
    using Severino.Domain.Shared.Commands;
    using Severino.Infrastructure.Data.Mongo;
    using Severino.Infrastructure.Monad;

    using static Severino.Infrastructure.Monad.Utils.Util;

    internal sealed class UpsertClient : Command<UpsertParam<string, Client>>, IUpsertClient
    {
        private readonly MongoConnection connection;

        public UpsertClient(MongoConnection connection) => this.connection = connection;

        protected override async Task<Try<Unit>> Execute(UpsertParam<string, Client> param)
        {
            await this.connection
                .Upsert<ClientDocument>(
                    item => item.Id == param.Id,
                    param.Entity);

            return Unit();
        }
    }
}
