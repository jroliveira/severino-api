namespace Severino.Domain.Client
{
    using Severino.Domain.Shared;
    using Severino.Infrastructure.ErrorHandling.Exceptions;
    using Severino.Infrastructure.Monad;

    using static Severino.Infrastructure.Monad.Utils.Util;

    public sealed class Secret : ValueObject<Secret, string>
    {
        private Secret(string value)
            : base(value)
        {
        }

        public static Try<Secret> NewSecret(in Option<string> value) =>
            value
                ? new Secret(value.Get())
                : Failure<Secret>(new InvalidObjectException("Invalid Secret."));
    }
}
