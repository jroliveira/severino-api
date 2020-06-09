namespace Severino.Domain.Client.Queries
{
    using Severino.Domain.Shared.Queries;
    using Severino.Infrastructure.Monad;
    using Severino.Infrastructure.Pagination;

    public interface IGetClients : IQuery<GetAllParam, Page<Try<Client>>>
    {
    }
}
