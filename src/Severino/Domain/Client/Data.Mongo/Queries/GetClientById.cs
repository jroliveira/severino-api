namespace Severino.Domain.Client.Data.Mongo.Queries
{
    using System.Linq;
    using System.Threading.Tasks;

    using Severino.Domain.Client.Data.Mongo.Documents;
    using Severino.Domain.Client.Queries;
    using Severino.Domain.Shared.Queries;
    using Severino.Infrastructure.Data.Mongo;
    using Severino.Infrastructure.Monad;

    internal sealed class GetClientById : Query<GetByIdParam<string>, Client>, IGetClientById
    {
        private readonly MongoConnection connection;

        public GetClientById(MongoConnection connection) => this.connection = connection;

        protected override async Task<Try<Client>> GetResult(GetByIdParam<string> param)
        {
            var entity = await this.connection
                .SingleOrDefault<ClientDocument>(client => client.Id == param.Id);

            return entity
                .Select(item => item.ToEntity());
        }
    }
}
