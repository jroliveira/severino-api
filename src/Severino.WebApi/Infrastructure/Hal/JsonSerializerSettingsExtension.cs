namespace Severino.WebApi.Infrastructure.Hal
{
    using Newtonsoft.Json;

    using Severino.WebApi.Infrastructure.Hal.Link;
    using Severino.WebApi.Infrastructure.Hal.Page;
    using Severino.WebApi.Infrastructure.Hal.Resource;

    internal static class JsonSerializerSettingsExtension
    {
        internal static JsonSerializerSettings AddHal(this JsonSerializerSettings @this)
        {
            var serializerSettings = @this;
            serializerSettings.Converters.Add(new ResourceJsonConverter());
            serializerSettings.Converters.Add(new LinksJsonConverter());
            serializerSettings.Converters.Add(new PageJsonConverter());

            return serializerSettings;
        }
    }
}
