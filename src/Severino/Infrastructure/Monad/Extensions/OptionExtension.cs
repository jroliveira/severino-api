namespace Severino.Infrastructure.Monad.Extensions
{
    using System.Linq;

    public static class OptionExtension
    {
        public static TValue GetOrElse<TValue>(this in Option<TValue> @this, TValue @default) => @this
            .Fold(@default)(_ => _);
    }
}
