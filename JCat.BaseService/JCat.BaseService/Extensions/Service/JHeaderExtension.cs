using JCat.BaseService.Config;
using JCat.BaseService.Const;
using JCat.BaseService.Extensions.BaseType;
using JCat.BaseService.MessageCenter;
using JCat.BaseService.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Net;

namespace JCat.BaseService.Extensions.Service
{
    public static class JHeaderExtension
    {
        public static Dictionary<string, object> AsDictionary(this JHeader baseHeaderResult)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            var props = baseHeaderResult.GetType().GetProperties();
            foreach (var prop in props)
            {
                var name = prop.Name.GetDisplayName<JHeader>();
                var value = prop.GetValue(baseHeaderResult, null);
                dict = dict.ToJCatDictionary(name, value);
            }
            return dict;
        }

        public static Dictionary<string, string> AsDictionary(this IHeaderDictionary headerDictionary)
        {
            if (headerDictionary.IsNull())
            {
                return new Dictionary<string, string>();
            }

            Dictionary<string, string> dict = new Dictionary<string, string>();
            var props = typeof(JHeader).GetProperties();
            foreach (var prop in props)
            {
                StringValues value;
                var name = prop.Name.GetDisplayName<JHeader>();
                var hasValue = headerDictionary.TryGetValue(name, out value);
                if (hasValue)
                {
                    dict.TryAdd(name, value);
                }
            }

            headerDictionary.TryGetValue(StringConst.Authorization, out var auth);
            if (auth.HasValue())
            {
                dict.Add(StringConst.Authorization, auth);
            }
            return dict;
        }

        public static bool IsHeaderMethodOptions(this HttpContext context)
        {
            if (context.Request.Method == StringConst.HeaderMethodOptions)
            {
                context.Response.StatusCode = 204;
                return true;
            }

            return false;
        }

        public static void SetJRequestBaseHeaders(this HttpContext context, string processId)
        {
            var header = new JHeader()
            {
                D_EventId = processId,
                D_Version = EnvironmentMode.GetVersion(),
                D_ts = DateTime.UtcNow,
                D_ReqTime = DateTime.UtcNow
            };
            var headerDict = header.AsDictionary();

            foreach (var item in headerDict)
            {
                if (item.Value != null)
                {
                    context.Request.Headers.TryAdd(item.Key.ToString(), item.Value.ToString());
                }
            }
        }

        public static void SetJResponseBaseHeaders(this HttpContext context)
        {
            var reqHeaders = context.Request.Headers.AsDictionary();
            var respTime = DateTime.UtcNow;
            foreach (var item in reqHeaders)
            {
                if (item.Value.IsNotNull())
                {
                    context.Response.Headers.TryAdd(item.Key, item.Value.ToString());
                }
            }

            var respTimePropName = nameof(JHeader.D_RespTime).GetDisplayName<JHeader>();
            if (context.Response.Headers.ContainsKey(respTimePropName))
            {
                context.Response.Headers.Remove(respTimePropName);
            }
            context.Response.Headers.TryAdd(respTimePropName, respTime.ToISO8601());
        }

        public static async Task SetJErrorResponseBaseHeaders(this HttpContext context, HttpStatusCode statusCode, string processId)
        {
            await JCatMiddlewareUtil.SetResponseType(context, statusCode);
            await JCatMiddlewareUtil.SetResponseEventId(context, processId);
        }

        public static async Task<object> GetResponseBody(this HttpContext context, MemoryStream ms)
        {
            var respContent = await JCatMiddlewareUtil.GetResponse<JResult>(ms);
            if (respContent.IsNull())
            {
                return new JResult(HttpStatusCode.NotFound, Message.NotFound, Message.ApiNotFound, null);
            }

            var systemDefaultResult = await JCatMiddlewareUtil.GetResponse<JSystemResult>(ms);
            if (respContent.Code.IsNull() && systemDefaultResult?.Status == default)
            {
                Message.JcatResultUsedError.ThrowArgumentNullException();
            }

            HttpStatusCode code;
            if (respContent.Code.IsNull())
            {
                code = (HttpStatusCode)systemDefaultResult.Status;
            }
            else
            {
                code = respContent.Code.Value;
            }

            await JCatMiddlewareUtil.SetResponseType(context, code);
            if (code.IsSuccessStatusCode())
            {
                return respContent.Data;
            }
            else if (systemDefaultResult.Status != default)
            {
                return new JErrorResult(systemDefaultResult.Title, systemDefaultResult.Errors?.ToString());
            }
            else
            {
                return new JErrorResult(respContent.Message, respContent.ErrorMessage, respContent.Data);
            }
        }

        public static bool IsSuccessStatusCode(this HttpStatusCode statusCode) => ((int)statusCode >= 200) && ((int)statusCode <= 299);
    }
}
