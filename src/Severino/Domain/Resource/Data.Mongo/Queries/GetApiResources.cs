namespace Severino.Domain.Resource.Data.Mongo.Queries
{
    using System.Linq;
    using System.Threading.Tasks;

    using Severino.Domain.Resource.Data.Mongo.Documents;
    using Severino.Domain.Resource.Queries;
    using Severino.Domain.Shared.Queries;
    using Severino.Infrastructure.Data.Mongo;
    using Severino.Infrastructure.Monad;
    using Severino.Infrastructure.Pagination;

    internal sealed class GetApiResources : Query<GetAllParam, Page<Try<ApiResource>>>, IGetApiResources
    {
        private readonly MongoConnection connection;

        public GetApiResources(MongoConnection connection) => this.connection = connection;

        protected override async Task<Try<Page<Try<ApiResource>>>> GetResult(GetAllParam param)
        {
            var entities = await this.connection
                .Where<ResourceDocument>(resource => resource.Type == "ApiResource");

            return new Page<Try<ApiResource>>(entities.Select(entity => entity.ToApiResourceEntity()), 0, 0);
        }
    }
}
