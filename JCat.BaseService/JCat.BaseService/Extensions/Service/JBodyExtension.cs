using JCat.BaseService.Const;
using JCat.BaseService.Converter;
using JCat.BaseService.Extensions.BaseType;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JCat.BaseService.Extensions.Service
{
    public static class JBodyExtension
    {
        public static Dictionary<string, string> AsDictionary(this IFormCollection formCollection)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var formKeys = formCollection?.Keys;
            foreach (var key in formKeys)
            {
                StringValues value;
                var hasValue = formCollection.TryGetValue(key, out value);
                if (hasValue)
                {
                    dict.Add(key, value);
                }
            }
            return dict;
        }

        public static Dictionary<string, string> AsDictionary(this IQueryCollection queryCollection)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var queryKeys = queryCollection?.Keys;
            foreach (var key in queryKeys)
            {
                StringValues value;
                var hasValue = queryCollection.TryGetValue(key, out value);
                if (hasValue)
                {
                    dict.Add(key, value);
                }
            }
            return dict;
        }

        public static string GetBodyString(this HttpRequest httpRequest)
        {
            httpRequest.EnableBuffering();
            string bodyString;
            // Encoding.UTF8
            var stream = new StreamReader(httpRequest.Body);
            stream.BaseStream.Seek(0, SeekOrigin.Begin);
            bodyString = stream.ReadToEndAsync().GetAwaiter().GetResult();
            stream.BaseStream.Seek(0, SeekOrigin.Begin);
            return bodyString;
        }

        public static string ToJsonString<T>(this T source) where T : new()
        {
            if (source.IsNull())
            {
                return StringConst.Empty;
            }

            var str = JCatSerializer.Serialize(source, JCatConverterSettings.GetBaseOptions());
            return str;
        }

        public static string ToNewtonJsonString<T>(this T source) where T : new()
        {
            if (source.IsNull())
            {
                return StringConst.Empty;
            }
            // bug : system.text.json / JsonSerializer / Serialize
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };
            settings.Converters.Add(new JavaScriptDateTimeConverter());
            var str = JsonConvert.SerializeObject(source, settings);
            return str;
        }
    }
}
