namespace Severino.Domain.Shared.Queries
{
    using Http.Query.Filter;

    using Severino.Domain.Shared;
    using Severino.Infrastructure.Monad;

    public sealed class GetAllParam : Param
    {
        private GetAllParam(Filter filter) => this.Filter = filter;

        public Filter Filter { get; }

        public static Try<GetAllParam> NewGetByAllParam(Filter filter) => new GetAllParam(filter);
    }
}
