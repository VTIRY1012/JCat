using JCat.BaseService.Extensions.BaseType;
using JCat.BaseService.MessageCenter;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JCat.BaseService.Converter
{
    public class JsonDecimalConverter : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.TokenType switch
            {
                JsonTokenType.Number => reader.GetDecimal(),
                JsonTokenType.String => reader.GetString().DecimalOrError(),
                _ => throw new NotSupportedException(Message.JsonNotSupportDecimelTypeError)
            };
        }

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }
}
