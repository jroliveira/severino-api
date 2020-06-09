namespace Severino.WebApi.Infrastructure.Authentication
{
    using System.Threading.Tasks;

    using IdentityServer4.Services;

    using static System.Threading.Tasks.Task;

    public sealed class CorsPolicyService : ICorsPolicyService
    {
        public Task<bool> IsOriginAllowedAsync(string origin) => FromResult(true);
    }
}
