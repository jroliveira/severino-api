namespace Severino.Infrastructure.ErrorHandling.Exceptions
{
    public sealed class AlreadyExistsException : BaseException
    {
        public AlreadyExistsException(string message)
            : base(message)
        {
        }
    }
}
