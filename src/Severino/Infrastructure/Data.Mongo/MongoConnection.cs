namespace Severino.Infrastructure.Data.Mongo
{
    using System;

    using Microsoft.Extensions.Options;

    using MongoDB.Driver;

    using static Severino.Infrastructure.ErrorHandling.ExceptionHandler;
    using static Severino.Infrastructure.Logging.Logger;

    public sealed class MongoConnection
    {
        private readonly IMongoDatabase? database;

        public MongoConnection(IOptions<MongoConfiguration> config)
        {
            try
            {
                this.database = new MongoClient(config.Value.Uri)
                    .GetDatabase(config.Value.Database);
            }
            catch (Exception exception)
            {
                LogError("Unable to connect to Mongo.", new { config.Value.Uri }, HandleException(exception));
            }
        }

        internal IMongoCollection<TDocument> GetCollection<TDocument>()
            where TDocument : class, new() => this.database
            .GetCollection<TDocument>(typeof(TDocument).Name);
    }
}
