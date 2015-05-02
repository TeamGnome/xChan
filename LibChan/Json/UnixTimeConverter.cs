using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LibChan.Json
{
    internal class UnixTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Integer)
            {
                throw new Exception("Value is not a JSON Integer");
            }

            long ticks = (long)reader.Value;

            return new DateTime(1970, 1, 1).AddSeconds(ticks);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if(!(value is DateTime))
            {
                throw new Exception("Value is not a DateTime");
            }

            TimeSpan time = (DateTime)value - new DateTime(1970, 1, 1);

            writer.WriteValue(Convert.ToInt64(time.TotalSeconds));
        }
    }
}
