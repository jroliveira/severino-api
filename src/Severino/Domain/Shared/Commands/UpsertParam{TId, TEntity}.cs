namespace Severino.Domain.Shared.Commands
{
    using Severino.Domain.Shared;
    using Severino.Infrastructure.ErrorHandling.Exceptions;
    using Severino.Infrastructure.Monad;

    using static Severino.Infrastructure.Monad.Utils.Util;

    public sealed class UpsertParam<TId, TEntity> : Param
        where TEntity : Entity<TId>
    {
        private UpsertParam(TId id, TEntity entity)
        {
            this.Id = id;
            this.Entity = entity;
        }

        public TId Id { get; }

        public TEntity Entity { get; }

        public static Try<UpsertParam<TId, TEntity>> NewUpsertParam(Option<TEntity> entity) => entity.Match(
            some => NewUpsertParam(some.Id, entity),
            () => Failure<UpsertParam<TId, TEntity>>(new InvalidObjectException("Invalid upsert param.")));

        public static Try<UpsertParam<TId, TEntity>> NewUpsertParam(
            in Option<TId> id,
            in Option<TEntity> entity) =>
                id
                && entity
                    ? new UpsertParam<TId, TEntity>(id.Get(), entity.Get())
                    : Failure<UpsertParam<TId, TEntity>>(new InvalidObjectException("Invalid upsert param."));
    }
}
