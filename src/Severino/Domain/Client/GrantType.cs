namespace Severino.Domain.Client
{
    using Severino.Domain.Shared;
    using Severino.Infrastructure.ErrorHandling.Exceptions;
    using Severino.Infrastructure.Monad;

    using static Severino.Infrastructure.Monad.Utils.Util;

    public sealed class GrantType : ValueObject<GrantType, string>
    {
        private GrantType(string name)
            : base(name)
        {
        }

        public static Try<GrantType> NewGrantType(in Option<string> name) =>
            name
                ? new GrantType(name.Get())
                : Failure<GrantType>(new InvalidObjectException("Invalid grant type."));
    }
}
