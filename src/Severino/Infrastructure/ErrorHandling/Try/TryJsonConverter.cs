﻿namespace Severino.Infrastructure.ErrorHandling.Try
{
    using System;

    using Newtonsoft.Json;

    using Severino.Infrastructure.Monad;

    using static Newtonsoft.Json.Linq.JObject;

    public sealed class TryJsonConverter : JsonConverter<ITry<object>>
    {
        public override void WriteJson(JsonWriter writer, ITry<object> value, JsonSerializer serializer) => value
            .Match(
                _ => FromObject(_, serializer),
                _ => FromObject(_, serializer))
            .WriteTo(writer);

        public override ITry<object> ReadJson(
            JsonReader reader,
            Type objectType,
            ITry<object> existingValue,
            bool hasExistingValue,
            JsonSerializer serializer) => throw new NotImplementedException();
    }
}
