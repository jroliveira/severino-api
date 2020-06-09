namespace Severino.WebApi.Infrastructure.Hal.Resource
{
    using Severino.WebApi.Infrastructure.Hal.Link;

    internal abstract class Resource : IResource
    {
        private readonly Links links;

        protected Resource(Links links) => this.links = links;

        public Links GetLinks() => this.links;
    }
}
