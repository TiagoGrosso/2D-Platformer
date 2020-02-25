using System;
using UnityEngine;
using Newtonsoft.Json.Linq;

namespace Newtonsoft.Json.Unity {
    public class JsonColorConverter : JsonConverter<Color> {
        public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
        {
            JObject j = new JObject { { "r", value.r }, { "g", value.g }, { "b", value.b }, { "a", value.a } };

            j.WriteTo(writer);
        }

        //CanRead is false which means the default implementation will be used instead.
        public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return existingValue;
        }

        public override bool CanWrite => true;

        public override bool CanRead => false;
    }
}