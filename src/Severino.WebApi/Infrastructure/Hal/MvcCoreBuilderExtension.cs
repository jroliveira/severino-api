namespace Severino.WebApi.Infrastructure.Hal
{
    using System.Linq;

    using Microsoft.Extensions.DependencyInjection;

    using Newtonsoft.Json;

    using Severino.WebApi.Infrastructure.Hal.Resource;

    using static System.Activator;
    using static System.AppDomain;

    using static Severino.WebApi.Infrastructure.Hal.ResourceBuilders;

    internal static class MvcCoreBuilderExtension
    {
        internal static IMvcCoreBuilder AddHal(this IMvcCoreBuilder @this, JsonSerializerSettings serializerSettings)
        {
            NewResourceBuilders(CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(IResourceBuilder).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract)
                .Select(type => CreateInstance(type) as IResourceBuilder)
                .SelectMany(item => item?.Builders)
                .ToDictionary(item => item.Key, item => item.Value));

            return @this
                .AddMvcOptions(options => options
                    .OutputFormatters
                    .Add(new HalJsonOutputFormatter(serializerSettings)));
        }
    }
}
