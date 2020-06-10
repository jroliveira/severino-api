namespace Severino.Domain.Claim
{
    using Severino.Domain.Shared;
    using Severino.Infrastructure.ErrorHandling.Exceptions;
    using Severino.Infrastructure.Monad;

    using static Severino.Infrastructure.Monad.Utils.Util;

    public sealed class Claim : ValueObject<Claim, string>
    {
        private Claim(string name)
            : base(name)
        {
        }

        public static Try<Claim> NewClaim(in Option<string> name) =>
            name
                ? new Claim(name.Get())
                : Failure<Claim>(new InvalidObjectException("Invalid claim."));
    }
}
