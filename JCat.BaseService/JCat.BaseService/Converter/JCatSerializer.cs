using JCat.BaseService.Extensions.BaseType;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace JCat.BaseService.Converter
{
    public static class JCatSerializer
    {
        [return: MaybeNull]
        public static TResult Deserialize<TResult>(string value)
        {
            if (value.IsNull())
            {
                return default(TResult);
            }

            var result = JsonSerializer.Deserialize<TResult>(value, JCatConverterSettings.GetBaseOptions());
            return result;
        }

        [return: MaybeNull]
        public static string Serialize<TValue>(TValue value, JsonSerializerOptions? options = null) where TValue : new()
        {
            if (value.IsNull())
            {
                return default;
            }

            var result = JsonSerializer.Serialize(value, options);
            return result;
        }

        [return: MaybeNull]
        public static TResult DeepCopy<TResult, TValue>(TValue value) where TValue : new()
        {
            var serialize = Serialize(value);
            var deserialize = Deserialize<TResult>(serialize);
            return deserialize;
        }

        [return: MaybeNull]
        public static Dictionary<string, object> ToDictionary<TValue>(TValue value) where TValue : new()
        {
            var jsonString = Serialize(value);
            var dict = Deserialize<Dictionary<string, object>>(jsonString);
            return dict;
        }

        [return: MaybeNull]
        public static Dictionary<string, string> ToDictionaryString<TValue>(TValue value) where TValue : new()
        {
            var jsonString = Serialize(value);
            var dict = Deserialize<Dictionary<string, object>>(jsonString);
            var result = new Dictionary<string, string>();
            if (dict.IsNull())
            {
                return result;
            }

            foreach (var item in dict)
            {
                if (item.Value != null)
                {
                    result.Add(item.Key, item.Value.ToString());
                }
            }
            return result;
        }
    }
}
