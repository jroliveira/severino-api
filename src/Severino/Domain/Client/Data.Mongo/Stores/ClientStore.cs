namespace Severino.Domain.Client.Data.Mongo.Stores
{
    using System.Threading.Tasks;

    using IdentityServer4.Models;
    using IdentityServer4.Stores;

    using Severino.Domain.Client.Data.Mongo.Documents;
    using Severino.Infrastructure.Data.Mongo;

    internal sealed class ClientStore : IClientStore
    {
        private readonly MongoConnection connection;

        public ClientStore(MongoConnection connection) => this.connection = connection;

        public async Task<Client?> FindClientByIdAsync(string clientId)
        {
            var document = await this.connection
                .SingleOrDefault<ClientDocument>(client => client.Id == clientId);

            return document.Get();
        }
    }
}
