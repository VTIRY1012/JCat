using System.Net;

namespace JCat.Cache.Redis.Model;
internal class DefaultResult
{
    public DefaultResult()
    {

    }

    public DefaultResult(HttpStatusCode code, object? data)
    {
        this.Code = code;
        this.Data = data;
    }

    public DefaultResult(HttpStatusCode code, string message, string ErrorMessage, object? data)
    {
        this.Code = code;
        this.Message = message;
        this.ErrorMessage = ErrorMessage;
        this.Data = data;
    }

    /// <summary>
    /// Return Http Status
    /// </summary>
    public HttpStatusCode? Code { get; set; }

    /// <summary>
    /// Response
    /// </summary>
    public Object? Data { get; set; }

    /// <summary>
    /// Response Message
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Response Backend Message
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;
}