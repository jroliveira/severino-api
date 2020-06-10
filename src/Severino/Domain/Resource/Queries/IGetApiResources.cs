namespace Severino.Domain.Resource.Queries
{
    using Severino.Domain.Shared.Queries;
    using Severino.Infrastructure.Monad;
    using Severino.Infrastructure.Pagination;

    public interface IGetApiResources : IQuery<GetAllParam, Page<Try<ApiResource>>>
    {
    }
}
