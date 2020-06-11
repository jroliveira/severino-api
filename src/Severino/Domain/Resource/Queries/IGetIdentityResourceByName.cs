namespace Severino.Domain.Resource.Queries
{
    using Severino.Domain.Shared.Queries;

    public interface IGetIdentityResourceByName : IQuery<GetByIdParam<string>, IdentityResource>
    {
    }
}
