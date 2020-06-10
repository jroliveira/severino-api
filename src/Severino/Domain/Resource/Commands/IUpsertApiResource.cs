namespace Severino.Domain.Resource.Commands
{
    using Severino.Domain.Shared.Commands;

    public interface IUpsertApiResource : ICommand<UpsertParam<string, ApiResource>>
    {
    }
}
