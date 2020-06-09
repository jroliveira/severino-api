namespace Severino.WebApi.Infrastructure.Hal.Resource
{
    using Severino.WebApi.Infrastructure.Hal.Link;

    internal interface IResource
    {
        Links GetLinks();
    }
}
