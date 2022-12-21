using JCat.BaseService.Converter;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text;

namespace JCat.Client.Extensions;
internal static class ContentExtension
{
    public static StringContent ToStringContent<TData>(this TData param)
    {
        var json = JCatSerializer.Serialize(param);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        return data;
    }

    public static void MemoryStreamDispose(this MemoryStream memoryStreams)
    {
        memoryStreams.Dispose();
    }

    public static void MemoryStreamDispose(this List<MemoryStream> memoryStreams)
    {
        foreach (var ms in memoryStreams)
        {
            ms.Dispose();
        }
    }

    public static MultipartFormDataContent FormContent(this MultipartFormDataContent content, Dictionary<string, object> formContent)
    {
        var strDict = JCatSerializer.ToDictionaryString(formContent);
        foreach (var item in strDict)
        {
            content.Add(new StringContent(item.Value), item.Key);
        }
        return content;
    }

    public static MemoryStream FileToContent(this MultipartFormDataContent content, string keyName, IFormFile formFile)
    {
        var ms = new MemoryStream();
        if (formFile == null)
        {
            return ms;
        }

        formFile.CopyTo(ms);
        var bac = new ByteArrayContent(ms.ToArray());
        if (!string.IsNullOrWhiteSpace(formFile?.ContentType))
        {
            bac.Headers.ContentType = new MediaTypeHeaderValue(formFile.ContentType);
        }
        content.Add(bac, keyName, formFile.FileName);
        return ms;
    }

    public static List<MemoryStream> FileToContent(this MultipartFormDataContent content, IEnumerable<IFormFile> formFiles)
    {
        var msList = new List<MemoryStream>();
        var i = 0;
        foreach (var file in formFiles)
        {
            i++;
            var ms = new MemoryStream();
            file.CopyTo(ms);
            var bac = new ByteArrayContent(ms.ToArray());
            if (!string.IsNullOrWhiteSpace(file?.ContentType))
            {
                bac.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            }
            content.Add(bac, $"file_{i}", file.FileName);
            msList.Add(ms);
        }
        return msList;
    }
}