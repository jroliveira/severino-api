namespace Severino.Domain.User.Data.Mongo.Queries
{
    using System.Linq;
    using System.Threading.Tasks;

    using Severino.Domain.Shared;
    using Severino.Domain.Shared.Queries;
    using Severino.Domain.User.Data.Mongo.Documents;
    using Severino.Domain.User.Queries;
    using Severino.Infrastructure.Data.Mongo;
    using Severino.Infrastructure.Monad;

    internal sealed class GetUserByEmail : Query<GetByIdParam<Email>, User>, IGetUserByEmail
    {
        private readonly MongoConnection connection;

        public GetUserByEmail(MongoConnection connection) => this.connection = connection;

        protected override async Task<Try<User>> GetResult(GetByIdParam<Email> param)
        {
            var entity = await this.connection
                .SingleOrDefault<UserDocument>(user => user.Email == param.Id);

            return entity
                .Select(item => item.ToEntity());
        }
    }
}
