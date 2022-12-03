using JCat.BaseService.Const;
using JCat.BaseService.Converter;
using JCat.BaseService.Extensions.BaseType;
using JCat.BaseService.Extensions.Service;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;
using System.Text.Json;

namespace JCat.BaseService.Util
{
    internal class JCatMiddlewareUtil
    {
        // Response Header
        internal static async Task SetCors(HttpContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { (string)context.Request.Headers["Origin"] });
            context.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Origin", "X-Requested-With", "Content-Type", "Accept", "ApiKey", "Authorization", });
            context.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "GET", "POST", "PUT", "DELETE", "OPTIONS" });
            context.Response.Headers.Add("Access-Control-Allow-Credentials", new[] { "true" });
            await Task.CompletedTask;
        }

        internal static async Task SetResponseType(HttpContext context, HttpStatusCode code)
        {
            context.Response.ContentType = "application/json;charset=utf-8";
            context.Response.StatusCode = (int)code;
            await Task.CompletedTask;
        }

        internal static async Task SetResponseEventId(HttpContext context, string processId)
        {
            context.Response.Headers.TryAdd(StringConst.HeaderEventId, processId);
            await Task.CompletedTask;
        }

        // Body
        [return: MaybeNull]
        internal static async Task<T> GetResponse<T>(MemoryStream ms)
        {
            ms.Seek(0, SeekOrigin.Begin);
            var respBody = await new StreamReader(ms).ReadToEndAsync();
            if (respBody.IsNullOrWhiteSpace())
            {
                return default(T);
            }

            var respResult = JCatSerializer.Deserialize<T>(respBody);
            return respResult;
        }

        internal static async Task SetOriginalRespBodyStream(HttpStatusCode code, object responseObj, Stream originalRespBodyStream, bool isSerialize = true)
        {
            var responseString = "";
            if (code.IsSuccessStatusCode())
            {
                responseString = SerializeFilter(responseObj, isSerialize);
            }
            else
            {
                responseString = JsonSerializer.Serialize(responseObj, JsonSettings.AddIgnoreNullValues(JCatConverterSettings.GetBaseOptions()));
            }

            if (responseString.IsNull())
            {
                responseString = "null";
            }
            var bytes = Encoding.UTF8.GetBytes(responseString);
            await originalRespBodyStream.WriteAsync(bytes, 0, bytes.Length);
        }

        internal static async Task ClearOriginalRespStream(Stream originalRespBodyStream)
        {
            await originalRespBodyStream.FlushAsync();
            await originalRespBodyStream.DisposeAsync();
        }

        [return: MaybeNull]
        private static string SerializeFilter(object data, bool isSerialize)
        {
            var element = (JsonElement?)data;
            if (!element.HasValue)
            {
                return JCatSerializer.Serialize(data, JCatConverterSettings.GetBaseOptions());
            }

            if (!isSerialize ||
                element.Value.ValueKind == JsonValueKind.Array ||
                element.Value.ValueKind == JsonValueKind.Object)
            {
                return data.ToString();
            }

            return JCatSerializer.Serialize(data, JCatConverterSettings.GetBaseOptions());
        }
    }
}
