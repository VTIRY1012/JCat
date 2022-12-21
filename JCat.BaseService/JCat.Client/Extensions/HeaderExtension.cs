using JCat.BaseService;
using JCat.BaseService.Extensions.BaseType;
using System.Net.Http.Headers;

namespace JCat.Client.Extensions;
internal static class HeaderExtension
{
    public static string GetEventId(this HttpResponseHeaders? headers)
    {
        if (headers == null)
        {
            return string.Empty;
        }
        var name = nameof(JHeader.D_EventId).GetDisplayName<JHeader>();
        return GetHeaderBase(headers, name);
    }

    public static string GetVersion(this HttpResponseHeaders? headers)
    {
        if (headers == null)
        {
            return string.Empty;
        }
        var name = nameof(JHeader.D_Version).GetDisplayName<JHeader>();
        return GetHeaderBase(headers, name);
    }

    public static bool GetIsFromCache(this HttpResponseHeaders? headers)
    {
        if (headers == null)
        {
            return false;
        }

        var name = nameof(JHeader.D_IsFromCache).GetDisplayName<JHeader>();
        var valueString = GetHeaderBase(headers, name);
        if (string.IsNullOrWhiteSpace(valueString))
        {
            return false;
        }
        var isSuccessed = bool.TryParse(valueString, out var result);
        if (!isSuccessed)
        {
            return false;
        }

        return result;
    }

    public static string GetCacheKey(this HttpResponseHeaders? headers)
    {
        if (headers == null)
        {
            return string.Empty;
        }
        var name = nameof(JHeader.D_CacheKey).GetDisplayName<JHeader>();
        return GetHeaderBase(headers, name);
    }

    public static string GetHeaderBase(this HttpResponseHeaders headers, string parameterName)
    {
        var values = Enumerable.Empty<string>();
        headers?.TryGetValues(parameterName, out values);
        return values?.SingleOrDefault();
    }
}