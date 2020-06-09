namespace Severino.Infrastructure.Data.Mongo
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using MongoDB.Driver;

    using Severino.Infrastructure.ErrorHandling.Exceptions;
    using Severino.Infrastructure.Monad;

    public static partial class LinqExtension
    {
        internal static async Task<Try<TDocument>> SingleOrDefault<TDocument>(
            this MongoConnection @this,
            Expression<Func<TDocument, bool>> predicate)
            where TDocument : class, IDocument, new()
        {
            try
            {
                var document = await @this
                    .GetCollection<TDocument>()
                    .Find(predicate)
                    .SingleOrDefaultAsync();

                if (document == null)
                {
                    return new NullObjectException("Document is null.");
                }

                return document;
            }
            catch (Exception exception)
            {
                return new InternalException("Could not run command in Mongo.", exception);
            }
        }

        internal static async Task<IReadOnlyCollection<TDocument>> Where<TDocument>(
            this MongoConnection @this,
            Expression<Func<TDocument, bool>> predicate)
            where TDocument : class, IDocument, new() => await @this
                .GetCollection<TDocument>()
                .Find(predicate)
                .ToListAsync();

        internal static async Task<IReadOnlyCollection<TDocument>> All<TDocument>(
            this MongoConnection @this)
            where TDocument : class, IDocument, new() => await @this
                .GetCollection<TDocument>()
                .Find(Builders<TDocument>.Filter.Empty)
                .ToListAsync();

        internal static Task RemoveAll<TDocument>(
            this MongoConnection @this,
            Expression<Func<TDocument, bool>> predicate)
            where TDocument : class, IDocument, new() => @this
                .GetCollection<TDocument>()
                .DeleteManyAsync(predicate);

        internal static Task Add<TDocument>(
            this MongoConnection @this,
            TDocument item)
            where TDocument : class, IDocument, new() => @this
                .GetCollection<TDocument>()
                .InsertOneAsync(item);

        internal static Task AddRange<TDocument>(
            this MongoConnection @this,
            IEnumerable<TDocument> collection)
            where TDocument : class, IDocument, new() => @this
                .GetCollection<TDocument>()
                .InsertManyAsync(collection);

        internal static Task Upsert<TDocument>(
            this MongoConnection @this,
            Expression<Func<TDocument, bool>> predicate,
            TDocument replacement)
            where TDocument : class, IDocument, new() => @this
                .GetCollection<TDocument>()
                .ReplaceOneAsync(
                    predicate,
                    replacement,
                    new UpdateOptions
                    {
                        IsUpsert = true,
                    });
    }
}
