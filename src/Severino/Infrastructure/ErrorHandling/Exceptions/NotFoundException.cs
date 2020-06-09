namespace Severino.Infrastructure.ErrorHandling.Exceptions
{
    public sealed class NotFoundException : BaseException
    {
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}
