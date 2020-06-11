﻿namespace Severino.Domain.Resource.Data.Mongo.Commands
{
    using System.Threading.Tasks;

    using Severino.Domain.Resource.Commands;
    using Severino.Domain.Resource.Data.Mongo.Documents;
    using Severino.Domain.Shared.Commands;
    using Severino.Infrastructure.Data.Mongo;
    using Severino.Infrastructure.Monad;

    using static Severino.Infrastructure.Monad.Utils.Util;

    internal sealed class UpsertIdentityResource : Command<UpsertParam<string, IdentityResource>>, IUpsertIdentityResource
    {
        private readonly MongoConnection connection;

        public UpsertIdentityResource(MongoConnection connection) => this.connection = connection;

        protected override async Task<Try<Unit>> Execute(UpsertParam<string, IdentityResource> param)
        {
            await this.connection
                .Upsert<ResourceDocument>(
                    item => item.Name == param.Id,
                    param.Entity);

            return Unit();
        }
    }
}
