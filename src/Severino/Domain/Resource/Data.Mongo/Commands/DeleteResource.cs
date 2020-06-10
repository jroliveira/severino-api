namespace Severino.Domain.Resource.Data.Mongo.Commands
{
    using System.Threading.Tasks;

    using Severino.Domain.Resource.Commands;
    using Severino.Domain.Resource.Data.Mongo.Documents;
    using Severino.Domain.Shared.Commands;
    using Severino.Infrastructure.Data.Mongo;
    using Severino.Infrastructure.Monad;

    using static Severino.Infrastructure.Monad.Utils.Util;

    internal sealed class DeleteResource : Command<DeleteParam<string>>, IDeleteResource
    {
        private readonly MongoConnection connection;

        public DeleteResource(MongoConnection connection) => this.connection = connection;

        protected override async Task<Try<Unit>> Execute(DeleteParam<string> param)
        {
            await this.connection
                .RemoveAll<ResourceDocument>(resource => resource.Name == param.Id);

            return Unit();
        }
    }
}
