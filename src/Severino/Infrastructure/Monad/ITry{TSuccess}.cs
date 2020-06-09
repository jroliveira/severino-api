namespace Severino.Infrastructure.Monad
{
    using System;

    using Severino.Infrastructure.ErrorHandling.Exceptions;

    public interface ITry<out TSuccess>
    {
        TSuccess Get();

        TReturn Match<TReturn>(Func<BaseException, TReturn> failure, Func<TSuccess, TReturn> success);
    }
}
