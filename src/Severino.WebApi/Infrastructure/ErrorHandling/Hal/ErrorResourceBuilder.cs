namespace Severino.WebApi.Infrastructure.ErrorHandling.Hal
{
    using System.Collections.Generic;

    using Severino.Infrastructure.Monad;
    using Severino.WebApi.Features.Shared.Hal;
    using Severino.WebApi.Infrastructure.Hal.Link;

    internal sealed class ErrorResourceBuilder : ResourceBuilder
    {
        internal override IReadOnlyCollection<IResourceConfiguration> Configure() => new List<IResourceConfiguration>
        {
            new ResourceConfiguration<Unit>((_, model) => new List<Link>()),
        };
    }
}
