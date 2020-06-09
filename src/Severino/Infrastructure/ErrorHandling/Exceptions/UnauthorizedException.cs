namespace Severino.Infrastructure.ErrorHandling.Exceptions
{
    public sealed class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message)
            : base(message)
        {
        }
    }
}
