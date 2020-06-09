﻿namespace Severino.Infrastructure.Monad.Utils
{
    using Severino.Infrastructure.ErrorHandling.Exceptions;

    public static partial class Util
    {
        public static Try<TSuccess> Success<TSuccess>(TSuccess success) => new Try<TSuccess>(success);

        public static Try<TValue> Failure<TValue>(BaseException exception) => new Try<TValue>(exception);
    }
}
