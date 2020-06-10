namespace Severino.Domain.Resource.Queries
{
    using Severino.Domain.Shared.Queries;

    public interface IGetApiResourceByName : IQuery<GetByIdParam<string>, ApiResource>
    {
    }
}
