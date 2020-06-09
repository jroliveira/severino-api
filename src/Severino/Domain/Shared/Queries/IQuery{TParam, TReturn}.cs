namespace Severino.Domain.Shared.Queries
{
    using System.Threading.Tasks;

    using Severino.Domain.Shared;
    using Severino.Infrastructure.Monad;

    public interface IQuery<TParam, TReturn>
        where TParam : Param
    {
        Task<Try<TReturn>> GetResult(in Option<TParam> param);
    }
}
