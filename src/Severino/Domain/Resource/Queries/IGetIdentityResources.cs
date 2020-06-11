namespace Severino.Domain.Resource.Queries
{
    using Severino.Domain.Shared.Queries;
    using Severino.Infrastructure.Monad;
    using Severino.Infrastructure.Pagination;

    public interface IGetIdentityResources : IQuery<GetAllParam, Page<Try<IdentityResource>>>
    {
    }
}
