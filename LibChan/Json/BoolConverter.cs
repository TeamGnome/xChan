using System;
using Newtonsoft.Json;

namespace LibChan.Json
{
    internal class BoolConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if(reader.TokenType != JsonToken.String)
            {
                throw new Exception("Value is not a JSON string.");
            }

            string val = reader.Value as string;

            return val == "1";
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is bool))
            {
                throw new Exception("Value is not a boolean");
            }

            string valueStr = (bool)value ? "1" : "0";

            writer.WriteValue(valueStr);
        }
    }
}
