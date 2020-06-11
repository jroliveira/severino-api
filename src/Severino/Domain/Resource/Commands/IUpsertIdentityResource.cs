namespace Severino.Domain.Resource.Commands
{
    using Severino.Domain.Shared.Commands;

    public interface IUpsertIdentityResource : ICommand<UpsertParam<string, IdentityResource>>
    {
    }
}
