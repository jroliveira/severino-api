namespace Severino.Domain.User.Queries
{
    using Severino.Domain.Shared;
    using Severino.Domain.Shared.Queries;

    public interface IGetUserByEmail : IQuery<GetByIdParam<Email>, User>
    {
    }
}
