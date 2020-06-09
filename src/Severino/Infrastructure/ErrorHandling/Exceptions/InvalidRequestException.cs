namespace Severino.Infrastructure.ErrorHandling.Exceptions
{
    public sealed class InvalidRequestException : BaseException
    {
        public InvalidRequestException(string message)
            : base(message)
        {
        }
    }
}
