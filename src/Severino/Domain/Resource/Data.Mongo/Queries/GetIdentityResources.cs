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

    internal sealed class GetIdentityResources : Query<GetAllParam, Page<Try<IdentityResource>>>, IGetIdentityResources
    {
        private readonly MongoConnection connection;

        public GetIdentityResources(MongoConnection connection) => this.connection = connection;

        protected override async Task<Try<Page<Try<IdentityResource>>>> GetResult(GetAllParam param)
        {
            var entities = await this.connection
                .Where<ResourceDocument>(resource => resource.Type == "IdentityResource");

            return new Page<Try<IdentityResource>>(entities.Select(entity => entity.ToIdentityResourceEntity()), 0, 0);
        }
    }
}
