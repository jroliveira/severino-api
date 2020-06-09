namespace Severino.Infrastructure.ErrorHandling.Exceptions
{
    public sealed class BusinessException : BaseException
    {
        public BusinessException(string message)
            : base(message)
        {
        }
    }
}
