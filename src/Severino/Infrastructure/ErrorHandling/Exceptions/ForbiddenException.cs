namespace Severino.Infrastructure.ErrorHandling.Exceptions
{
    public sealed class ForbiddenException : BaseException
    {
        public ForbiddenException(string message)
            : base(message)
        {
        }
    }
}
