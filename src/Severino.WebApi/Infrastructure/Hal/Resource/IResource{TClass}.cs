namespace Severino.WebApi.Infrastructure.Hal.Resource
{
    internal interface IResource<out TClass> : IResource
        where TClass : class
    {
        TClass Get();
    }
}
