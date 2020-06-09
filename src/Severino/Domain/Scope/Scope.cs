namespace Severino.Domain.Scope
{
    using Severino.Domain.Shared;
    using Severino.Infrastructure.ErrorHandling.Exceptions;
    using Severino.Infrastructure.Monad;

    using static Severino.Infrastructure.Monad.Utils.Util;

    public sealed class Scope : ValueObject<Scope, string>
    {
        private Scope(string name)
            : base(name)
        {
        }

        public static Try<Scope> NewScope(in Option<string> name) => name
            ? new Scope(name.Get())
            : Failure<Scope>(new InvalidObjectException("Invalid scope."));
    }
}
