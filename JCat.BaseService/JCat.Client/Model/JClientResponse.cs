using System.Net;

namespace JCat.Client.Model;
public class JClientResponse<TData>
{
    public bool IsSuccessStatusCode { get; set; } = true;
    public bool IsFromCache { get; set; } = false;
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    public string CacheKey { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string EventId { get; set; } = string.Empty;
    public TData? Data { get; set; }
    public ErrorServiceClientResponse ErrorResponse { get; set; } = new ErrorServiceClientResponse();
}

public class ErrorServiceClientResponse
{
    /// <summary>
    /// Frontend Message
    /// </summary>
    public string Message { get; set; } = string.Empty;
    /// <summary>
    /// Backend Message
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;
    public string StackTrace { get; set; } = string.Empty;
}