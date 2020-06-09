namespace Severino.Domain.Client.Data.Mongo.Queries
{
    using System.Linq;
    using System.Threading.Tasks;

    using Severino.Domain.Client.Data.Mongo.Documents;
    using Severino.Domain.Client.Queries;
    using Severino.Domain.Shared.Queries;
    using Severino.Infrastructure.Data.Mongo;
    using Severino.Infrastructure.Monad;
    using Severino.Infrastructure.Pagination;

    internal sealed class GetClients : Query<GetAllParam, Page<Try<Client>>>, IGetClients
    {
        private readonly MongoConnection connection;

        public GetClients(MongoConnection connection) => this.connection = connection;

        protected override async Task<Try<Page<Try<Client>>>> GetResult(GetAllParam param)
        {
            var entities = await this.connection
                .All<ClientDocument>();

            return new Page<Try<Client>>(entities.Select(entity => entity.ToEntity()), 0, 0);
        }
    }
}
