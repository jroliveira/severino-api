namespace Severino.Domain.Client.Queries
{
    using Severino.Domain.Shared.Queries;

    public interface IGetClientById : IQuery<GetByIdParam<string>, Client>
    {
    }
}
