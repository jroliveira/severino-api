namespace Severino.Infrastructure.Serialization
{
    using System.Collections.Generic;
    using System.Globalization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

    using Severino.Infrastructure.ErrorHandling.Try;
    using Severino.Infrastructure.Serialization.Converters;

    public static class JsonSettings
    {
        public static JsonSerializerSettings JsonSerializerSettings => new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented,
            Culture = CultureInfo.InvariantCulture,
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            Converters = new List<JsonConverter>
            {
                new StringEnumConverter(),
                new IpAddressConverter(),
                new IpEndPointConverter(),
                new TryJsonConverter(),
            },
        };
    }
}
