namespace Severino.Infrastructure.Monad.Extensions
{
    using System.Linq;

    using Severino.Infrastructure.ErrorHandling.Exceptions;
    using Severino.Infrastructure.Monad.Utils;

    public static class OptionExtension
    {
        public static TValue GetOrElse<TValue>(this in Option<TValue> @this, TValue @default) => @this
            .Fold(@default)(_ => _);

        public static Try<TValue> ToTry<TValue>(this Option<TValue> @this) => @this.Match(
            value => value,
            () => Util.Failure<TValue>(new NullObjectException("Value is required.")));
    }
}
