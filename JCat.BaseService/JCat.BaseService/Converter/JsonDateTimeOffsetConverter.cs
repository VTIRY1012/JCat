using JCat.BaseService.MessageCenter;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JCat.BaseService.Converter
{
    public class JsonDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Debug.Assert(typeToConvert == typeof(DateTimeOffset));
            var flag = DateTimeOffset.TryParse(reader.GetString(), out var value);
            if (flag)
            {
                return value;
            }

            throw new ArgumentException(Message.JsonDateTimeOffSetConverterArgError);
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            value = value.UtcDateTime;
            writer.WriteStringValue(value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssZ"));
        }
    }
}
