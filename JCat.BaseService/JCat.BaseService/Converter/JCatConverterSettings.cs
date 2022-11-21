using System.Text.Json;
using System.Text.Json.Serialization;

namespace JCat.BaseService.Converter
{
    public static class JCatConverterSettings
    {
        public static JsonSerializerOptions GetBaseOptions()
        {
            var options = _baseOption;
            options.AddBaseConverters();
            options.Converters.Add(new JsonDecimalConverter());
            return options;
        }

        private static JsonSerializerOptions _baseOption = new JsonSerializerOptions()
        {
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            IgnoreReadOnlyProperties = true,
            WriteIndented = true
        };
    }

    public static class JsonSettings
    {
        public static void AddBaseJsonSettings(this JsonSerializerOptions options)
        {
            options.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.PropertyNameCaseInsensitive = true;
            options.IgnoreReadOnlyProperties = true;
            options.WriteIndented = true;
            options.AddBaseConverters();
        }

        public static JsonSerializerOptions AddIgnoreNullValues(this JsonSerializerOptions options)
        {
            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            return options;
        }

        public static void AddBaseConverters(this JsonSerializerOptions options)
        {
            options.Converters.Add(new JsonStringEnumConverter());
            options.Converters.Add(new JsonDateTimeConverter());
            options.Converters.Add(new JsonDateTimeOffsetConverter());
        }
    }
}
