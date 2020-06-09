namespace Severino.Domain.Client.Commands
{
    using Severino.Domain.Shared.Commands;

    public interface IDeleteClient : ICommand<DeleteParam<string>>
    {
    }
}
