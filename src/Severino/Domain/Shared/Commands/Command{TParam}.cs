namespace Severino.Domain.Shared.Commands
{
    using System.Threading.Tasks;

    using Severino.Domain.Shared;
    using Severino.Infrastructure.ErrorHandling.Exceptions;
    using Severino.Infrastructure.Monad;

    using static Severino.Infrastructure.Monad.Utils.Util;

    public abstract class Command<TParam> : ICommand<TParam>
        where TParam : Param
    {
        public Task<Try<Unit>> Execute(in Option<TParam> param) => param.Match(
            this.Execute,
            () => Task(Failure<Unit>(new NullObjectException("Param is required."))));

        protected abstract Task<Try<Unit>> Execute(TParam param);
    }
}
