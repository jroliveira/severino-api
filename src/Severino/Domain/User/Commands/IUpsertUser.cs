namespace Severino.Domain.User.Commands
{
    using Severino.Domain.Shared;
    using Severino.Domain.Shared.Commands;

    public interface IUpsertUser : ICommand<UpsertParam<Email, User>>
    {
    }
}
