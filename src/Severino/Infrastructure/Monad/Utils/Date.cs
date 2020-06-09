namespace Severino.Infrastructure.Monad.Utils
{
    using System;

    using Severino.Infrastructure.ErrorHandling.Exceptions;

    public static partial class Util
    {
        public static Try<DateTime> Date(
            in Option<int> year,
            in Option<int> month,
            in Option<int> day) =>
                year
                && month
                && day
                    ? new DateTime(year.Get(), month.Get(), day.Get())
                    : Failure<DateTime>(new InvalidObjectException("Invalid date."));
    }
}
