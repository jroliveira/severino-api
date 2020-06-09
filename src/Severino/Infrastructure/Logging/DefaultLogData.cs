﻿namespace Severino.Infrastructure.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.Serialization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

    using Severino.Infrastructure.ErrorHandling.Try;
    using Severino.Infrastructure.Serialization.Converters;

    using static Newtonsoft.Json.JsonConvert;

    using static Severino.Infrastructure.Clock;

    public sealed class DefaultLogData : ILogData
    {
        private readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.None,
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

        public DefaultLogData(LogLevel level, string message, object data)
        {
            this.Level = level;
            this.Message = message;
            this.Data = data;
        }

        public DateTime DateTime => UtcNow();

        public LogLevel Level { get; }

        public string Message { get; }

        public object Data { get; }

        public override string ToString()
        {
            try
            {
                return SerializeObject(this, this.jsonSerializerSettings);
            }
            catch (Exception exception)
            {
                throw new SerializationException("Cannot serialize object default log data.", exception);
            }
        }
    }
}
