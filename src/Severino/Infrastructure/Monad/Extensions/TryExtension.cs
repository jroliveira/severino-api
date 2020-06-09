namespace Severino.Infrastructure.Monad.Extensions
{
    using static Severino.Infrastructure.Monad.Utils.Util;

    public static class TryExtension
    {
        public static TValue GetOrElse<TValue>(this Try<TValue> @this, TValue @default) => @this.Match(
            _ => @default,
            value => value);

        public static Option<TSuccess> ToOption<TSuccess>(this Try<TSuccess> @this) => @this.Match(
            _ => None(),
            Some);
    }
}
