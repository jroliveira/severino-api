namespace Severino.Domain.User.Data.Mongo.Queries
{
    using System.Linq;
    using System.Threading.Tasks;

    using Severino.Domain.Shared.Queries;
    using Severino.Domain.User.Data.Mongo.Documents;
    using Severino.Domain.User.Queries;
    using Severino.Infrastructure.Data.Mongo;
    using Severino.Infrastructure.Monad;
    using Severino.Infrastructure.Pagination;

    internal sealed class GetUsers : Query<GetAllParam, Page<Try<User>>>, IGetUsers
    {
        private readonly MongoConnection connection;

        public GetUsers(MongoConnection connection) => this.connection = connection;

        protected override async Task<Try<Page<Try<User>>>> GetResult(GetAllParam param)
        {
            var entities = await this.connection
                .All<UserDocument>();

            return new Page<Try<User>>(entities.Select(entity => entity.ToEntity()), 0, 0);
        }
    }
}
