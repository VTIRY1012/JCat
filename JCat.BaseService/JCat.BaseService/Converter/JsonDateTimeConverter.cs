using JCat.BaseService.MessageCenter;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JCat.BaseService.Converter
{
    public sealed class JsonDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Debug.Assert(typeToConvert == typeof(DateTime));
            var flag = DateTime.TryParse(reader.GetString(), out var value);
            if (flag)
            {
                return value;
            }

            throw new ArgumentException(Message.JsonDateTimeConverterArgError);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssZ"));
        }
    }
}
