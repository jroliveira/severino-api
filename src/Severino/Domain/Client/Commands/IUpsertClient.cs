namespace Severino.Domain.Client.Commands
{
    using Severino.Domain.Shared.Commands;

    public interface IUpsertClient : ICommand<UpsertParam<string, Client>>
    {
    }
}
