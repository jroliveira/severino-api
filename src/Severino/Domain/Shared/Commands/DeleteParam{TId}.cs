namespace Severino.Domain.Shared.Commands
{
    using Severino.Domain.Shared;
    using Severino.Infrastructure.ErrorHandling.Exceptions;
    using Severino.Infrastructure.Monad;

    using static Severino.Infrastructure.Monad.Utils.Util;

    public sealed class DeleteParam<TId> : Param
    {
        private DeleteParam(TId id) => this.Id = id;

        public TId Id { get; }

        public static Try<DeleteParam<TId>> NewDeleteParam(in Option<TId> id) =>
            id
            ? new DeleteParam<TId>(id.Get())
            : Failure<DeleteParam<TId>>(new InvalidObjectException("Invalid delete param."));
    }
}
