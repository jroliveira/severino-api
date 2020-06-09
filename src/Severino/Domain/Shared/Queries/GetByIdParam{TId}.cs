namespace Severino.Domain.Shared.Queries
{
    using Severino.Domain.Shared;
    using Severino.Infrastructure.ErrorHandling.Exceptions;
    using Severino.Infrastructure.Monad;

    using static Severino.Infrastructure.Monad.Utils.Util;

    public sealed class GetByIdParam<TId> : Param
    {
        private GetByIdParam(TId id) => this.Id = id;

        public TId Id { get; }

        public static Try<GetByIdParam<TId>> NewGetByIdParam(in Option<TId> id) => id
            ? new GetByIdParam<TId>(id.Get())
            : Failure<GetByIdParam<TId>>(new InvalidObjectException("Invalid get by id param."));
    }
}
