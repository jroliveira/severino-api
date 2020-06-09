namespace Severino.Domain.Shared.Queries
{
    using System.Threading.Tasks;

    using Severino.Domain.Shared;
    using Severino.Infrastructure.ErrorHandling.Exceptions;
    using Severino.Infrastructure.Monad;

    using static Severino.Infrastructure.Monad.Utils.Util;

    public abstract class Query<TParam, TReturn> : IQuery<TParam, TReturn>
        where TParam : Param
    {
        public Task<Try<TReturn>> GetResult(in Option<TParam> param) => param.Match(
            this.GetResult,
            () => Task(Failure<TReturn>(new NullObjectException("Param is required."))));

        protected abstract Task<Try<TReturn>> GetResult(TParam param);
    }
}
