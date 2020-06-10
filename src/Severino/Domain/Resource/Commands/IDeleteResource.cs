namespace Severino.Domain.Resource.Commands
{
    using Severino.Domain.Shared.Commands;

    public interface IDeleteResource : ICommand<DeleteParam<string>>
    {
    }
}
