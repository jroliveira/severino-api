namespace Severino.Domain.Resource.Data.Mongo.Queries
{
    using System.Linq;
    using System.Threading.Tasks;

    using Severino.Domain.Resource.Data.Mongo.Documents;
    using Severino.Domain.Resource.Queries;
    using Severino.Domain.Shared.Queries;
    using Severino.Infrastructure.Data.Mongo;
    using Severino.Infrastructure.Monad;

    internal sealed class GetIdentityResourceByName : Query<GetByIdParam<string>, IdentityResource>, IGetIdentityResourceByName
    {
        private readonly MongoConnection connection;

        public GetIdentityResourceByName(MongoConnection connection) => this.connection = connection;

        protected override async Task<Try<IdentityResource>> GetResult(GetByIdParam<string> param)
        {
            var entity = await this.connection
                .SingleOrDefault<ResourceDocument>(resource => resource.Name == param.Id && resource.Type == "IdentityResource");

            return entity
                .Select(item => item.ToIdentityResourceEntity());
        }
    }
}
