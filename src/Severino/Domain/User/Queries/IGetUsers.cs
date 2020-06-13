namespace Severino.Domain.User.Queries
{
    using Severino.Domain.Shared.Queries;
    using Severino.Infrastructure.Monad;
    using Severino.Infrastructure.Pagination;

    public interface IGetUsers : IQuery<GetAllParam, Page<Try<User>>>
    {
    }
}
