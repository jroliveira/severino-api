namespace Severino.Domain.Shared.Commands
{
    using System.Threading.Tasks;

    using Severino.Domain.Shared;
    using Severino.Infrastructure.Monad;

    public interface ICommand<TParam>
        where TParam : Param
    {
        Task<Try<Unit>> Execute(in Option<TParam> param);
    }
}
