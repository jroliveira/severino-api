namespace Severino.Domain.Resource.Data.Mongo.Queries
{
    using System.Linq;
    using System.Threading.Tasks;

    using Severino.Domain.Resource.Data.Mongo.Documents;
    using Severino.Domain.Resource.Queries;
    using Severino.Domain.Shared.Queries;
    using Severino.Infrastructure.Data.Mongo;
    using Severino.Infrastructure.Monad;

    internal sealed class GetApiResourceByName : Query<GetByIdParam<string>, ApiResource>, IGetApiResourceByName
    {
        private readonly MongoConnection connection;

        public GetApiResourceByName(MongoConnection connection) => this.connection = connection;

        protected override async Task<Try<ApiResource>> GetResult(GetByIdParam<string> param)
        {
            var entity = await this.connection
                .SingleOrDefault<ResourceDocument>(resource => resource.Name == param.Id && resource.Type == "ApiResource");

            return entity
                .Select(item => item.ToApiResourceEntity());
        }
    }
}
