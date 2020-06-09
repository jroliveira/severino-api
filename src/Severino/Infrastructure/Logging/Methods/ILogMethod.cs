namespace Severino.Infrastructure.Logging.Methods
{
    public interface ILogMethod
    {
        void Write(LogLevel logLevel, string data);
    }
}
